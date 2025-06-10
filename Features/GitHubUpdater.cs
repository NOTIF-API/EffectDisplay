using System;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EffectDisplay.Features
{
    public class GithubUpdater
    {
        public const string API_Update = "https://api.github.com/repos/{owner}/{repo}/releases/latest";

        private string Owner { get; set; } = string.Empty;

        private string Repository { get; set; } = string.Empty;

        private Version LastDetected = new Version(0, 0, 0);

        public Version Version 
        { 
            get 
            { 
                return GetLatest(); 
            }
        }

        public GithubUpdater(string owner, string repository)
        {
            Owner = owner;
            Repository = repository;
        }

        private Version GetLatest()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage res = client.GetAsync(API_Update.Replace("{owner}", Owner).Replace("{repo}", Repository)).Result;
                    JObject js = JObject.Parse(res.Content.ReadAsStringAsync().Result);
                    return Version.Parse((string)js["tag_name"]);
                }
                catch (Exception ex)
                {
                    return new Version(0, 0, 0);
                }
            }
        }
    }
}
