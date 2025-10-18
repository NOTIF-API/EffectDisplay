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

        public static Task<Version> GetLatestAsync(string owner, string repository)
        {
            return Task.Run(async () =>
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage res = await client.GetAsync(API_Update.Replace("{owner}", owner).Replace("{repo}", repository));
                        JObject js = JObject.Parse(res.Content.ReadAsStringAsync().Result);
                        return Version.Parse((string)js["tag_name"]);
                    }
                    catch (Exception ex)
                    {
                        return new Version(0, 0, 0);
                    }
                }
            });
        }
    }
}