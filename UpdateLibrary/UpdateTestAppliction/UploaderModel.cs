using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UpdateTestAppliction
{
    public class UploaderModel
    {
        public string Login { get; set; }
        public string ID { get; set; }
        public string Node_ID { get; set; }
        public string Avatar_URL { get; set; }
        public string Gravatar_URL { get; set; }
        public string URL { get; set; }
        public string HTML_URL { get; set; }
        public string Followers_URL { get; set; }
        public string Subscriptions_URL { get; set; }
        public string Organisations_URL { get; set; }
        public string Repos_URL { get; set; }
        public string Received_Events_URL { get; set; }
        public string Type { get; set; }
        public string Site_Admin { get; set; }

        private string rawData;
        public UploaderModel(string rawData)
        {
            JObject obj = JObject.Parse(rawData);

            this.rawData = rawData;
            this.Login = (string)obj["login"];
            this.ID = (string)obj["id"]; 
            this.Node_ID = (string)obj["node_id"]; 
            this.Avatar_URL = (string)obj["avatar_url"]; 
            this.Gravatar_URL = (string)obj["gravatar_id"]; 
            this.URL = (string)obj["url"]; 
            this.HTML_URL = (string)obj["html_url"];
            this.Followers_URL = (string)obj["followers_url"]; 
            this.Subscriptions_URL = (string)obj["subscriptions_url"]; 
            this.Organisations_URL = (string)obj["organizations_url"];
            this.Repos_URL = (string)obj["repos_url"];
            this.Received_Events_URL = (string)obj["received_events_url"];
            this.Type = (string)obj["type"]; 
            this.Site_Admin = (string)obj["site_admin"];
        }


        private RegexOptions options = RegexOptions.Singleline;
        private string GetValue(string tag)
        {
            //"browser_download_url":".*"
            string pattern = $"\"{tag}\":.*,";
            Match m = Regex.Match(rawData, pattern, options);
            Console.WriteLine(m.Value);
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
