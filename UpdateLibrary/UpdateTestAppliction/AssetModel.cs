using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace UpdateTestAppliction
{
    public class AssetModel
    {
        public string URL { get; set; }
        public string ID { get; set; }
        public string Node_ID { get; set; }
        public string Name { get; set; }
        public string Lable { get; set; }
        public string Content_Type { get; set; }
        public string State { get; set; }
        public string Size { get; set; }
        public string Download_Count { get; set; }
        public string Created_At { get; set; }
        public string Uploaded_At { get; set; }
        public string Browser_Download_URL { get; set; }
        public UploaderModel Uploader { get; set; }

        string rawData = "";
        public AssetModel(string rawData)
        {
            this.rawData = rawData;

            JObject obj = JObject.Parse(rawData);

            this.URL = (string)obj["url"]; 
            this.ID = (string)obj["id"]; 
            this.Node_ID = (string)obj["node_id"]; 
            this.Name = (string)obj["name"]; 
            this.Lable = (string)obj["label"];
            this.Content_Type = (string)obj["content_type"];
            this.State = (string)obj["state"]; 
            this.Size = (string)obj["size"];
            this.Download_Count = (string)obj["download_count"]; 
            this.Created_At = (string)obj["created_at"]; 
            this.Uploaded_At = (string)obj["updated_at"];
            this.Browser_Download_URL = (string)obj["browser_download_url"];
            //this.Uploader = new UploaderModel(rawData);
        }

        private RegexOptions options = RegexOptions.Singleline;
        private string GetValue(string tag) 
        {
            //"browser_download_url":".*"
            string pattern = $"\"{tag}\":.*,";
            Match m = Regex.Match(rawData, pattern, options);
            Console.WriteLine("---------------");
            Console.WriteLine(m.Value);
            Console.WriteLine("---------------");
            //Match m = Regex.Match(rawData, pattern, options);
            return GetContent(m.Value);
        }
        private string GetContent(string Value) 
        {
            int firstTrimmer = Value.IndexOf(':');

            if (firstTrimmer < 0) return string.Empty;
            string tagValue = Value.Substring(firstTrimmer);

            int beginning = tagValue.Trim().IndexOf('"');

            if (beginning < 0)
                return tagValue.Trim();

            string message = tagValue.Trim().Substring(beginning).Trim('\"');
            return message.Trim();
        }
    }
}
