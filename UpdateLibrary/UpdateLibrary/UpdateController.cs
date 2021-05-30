using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace UpdateLibrary
{
    public class UpdateController
    {
        public delegate void OnDownloadProcessReportEventHandler(object sender, float percent);

        /// <summary>
        /// event is raised when process is reported
        /// </summary>
        public event OnDownloadProcessReportEventHandler OnDownloadReportProcess;

        public delegate void OnDownloadFinishedEventHandler(object sender, string downloadPath, string downloadURL);
        
        /// <summary>
        /// event is raised when download is finished
        /// </summary>
        public event OnDownloadFinishedEventHandler OnDownloadFinished;

        public delegate void OnDownloadStartedEventHandler(object sender, string downloadPath, string downloadURL, long downloadSize);

        /// <summary>
        /// event is raised when a download is started 
        /// </summary>
        public event OnDownloadStartedEventHandler OnDownloadStarted;

        /// <summary>
        /// repository owner
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// repository name
        /// </summary>
        public string Repository { get; set; }

        /// <summary>
        /// Constructor of UpdateController
        /// </summary>
        /// <param name="Username">Username of repository owner</param>
        /// <param name="Repository">Repository name</param>
        public UpdateController(string Username, string Repository)
        {
            this.Username = Username;
            this.Repository = Repository;
        }

        /// <summary>
        /// Request releases on GitHub
        /// </summary>
        /// <returns>returns a Response Model</returns>
        public Models.ResponseModel[] GetResponse() 
        {
            string requestString = $"https://api.github.com/repos/{Username}/{Repository}/releases";

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("User-Agent: request");
                var json = wc.DownloadString(requestString);
                return JsonConvert.DeserializeObject<Models.ResponseModel[]>(json);
            }
        }

        /// <summary>
        /// starts a asyncron download of a asset
        /// </summary>
        /// <param name="asset">github asset</param>
        /// <param name="path">path of the downloaded file</param>
        /// <param name="ReportProcessIntervall">time between downloadstate check</param>
        public void DownloadFileAsync(Models.AssetsModel asset, string path, int ReportProcessIntervall = 200) 
        {
            bool downloading = true;

            if (File.Exists(path))
                File.Delete(path);

            new Thread(() => 
            {
                while (downloading) 
                {
                    if(File.Exists(path))
                    {
                        try 
                        {
                            //File i = 
                            //Console.WriteLine(Files);
                            long size = new System.IO.FileInfo(path).Length;
                            long assetSize = asset.size;
                            float percentage = (size / assetSize) * 100;
                            OnDownloadReportProcess?.Invoke(this, percentage);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                    }

                    Thread.Sleep(ReportProcessIntervall);
                }
            }).Start();

            new Thread(() =>
            {
                OnDownloadStarted?.Invoke(this, path, asset.browser_download_url, asset.size);
                WebClient downloadClient = new WebClient();
                downloadClient.DownloadFile(asset.browser_download_url, path);
                OnDownloadFinished?.Invoke(this, path, asset.browser_download_url);
                downloading = false;
            }).Start();
        }
    }
}
