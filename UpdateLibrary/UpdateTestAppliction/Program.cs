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
            string repository = "UpdateLibrary";
            string accountName = "ManamanaTheReal499";

            UpdateController updateController = new UpdateController(accountName, repository);

            UpdateLibrary.Models.ResponseModel[] response = updateController.GetResponse();

            updateController.OnDownloadReportProcess += ReportProcess;
            updateController.OnDownloadStarted += DownloadStarted;
            updateController.OnDownloadFinished += DownloadFinished;

            string filepath = Directory.GetCurrentDirectory() + @"\" + response[0].assets[0].name;

            updateController.DownloadFileAsync(response[0].assets[0], filepath);

            Console.ReadKey();
        }

        private static void DownloadFinished(object sender, string downloadPath, string downloadURL)
        {
            Console.WriteLine("Download Finished");
        }

        private static void DownloadStarted(object sender, string downloadPath, string downloadURL, long downloadSize)
        {
            Console.WriteLine("Download Started");
        }

        private static void ReportProcess(object sender, float percent)
        {
            Console.WriteLine(percent);
        }
    }
}
