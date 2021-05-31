# UpdateLibrary
Install Newtonsoft.Json NuGet-Package

```c#
//name of the repository
string repository = "UpdateLibrary";

//owner username
string accountName = "ManamanaTheReal499";

//instance of UpdateController object
UpdateController updateController = new UpdateController(accountName, repository);

//get releases from github api
UpdateLibrary.Models.ResponseModel[] response = updateController.GetResponse();

//download events
updateController.OnDownloadReportProcess += ReportProcess;
updateController.OnDownloadStarted += DownloadStarted;
updateController.OnDownloadFinished += DownloadFinished;

//path where the file is stored
string filepath = Directory.GetCurrentDirectory() + @"\folder";

//creating folder
if (!Directory.Exists(filepath))
  Directory.CreateDirectory(filepath);

//downloading all assets from lastest release
updateController.DownloadFileAsync(response[0], filepath);
```

Update library c# .net 4.7
