using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using UpdateLibrary;

namespace UpdateTestAppliction
{
    class Program
    {
        static void Main(string[] args)
        {
            string accountName = "Shabinder";
            string repository = "SpotiFlyer";

            UpdateController updateController = new UpdateController(accountName, repository);

            UpdateLibrary.Models.ResponseModel[] response = updateController.GetResponse();

            updateController.OnDownloadReportProcess += ReportProcess;
            updateController.OnDownloadStarted += DownloadStarted;
            updateController.OnDownloadFinished += DownloadFinished;
            updateController.OnDownloadCompleted += DownloadCompleted;

            string filepath = Directory.GetCurrentDirectory() + @"\luis" ;

            if (!Directory.Exists(filepath))
                Directory.CreateDirectory(filepath);

            updateController.DownloadFileAsync(response[0], filepath);

            Console.ReadKey();
        }

        private static void DownloadCompleted(object sender)
        {
            Console.WriteLine("Finished");
        }

        private static void DownloadFinished(object sender, string downloadPath, string downloadURL)
        {
            Console.WriteLine(downloadPath);
        }

        private static void DownloadStarted(object sender, string downloadPath, string downloadURL, long downloadSize)
        {
            Console.WriteLine(downloadSize);
        }

        private static void ReportProcess(object sender, float percent)
        {
            Console.WriteLine(percent);
        }
    }
}
