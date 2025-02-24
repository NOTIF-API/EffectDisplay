using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EffectDisplay.Features
{
    public class GithubUpdater
    {
        private const string LatestString = "/releases/latest";
        private string _ProjectLink { get; set; }
        private Version CurrentVersion { get; set; }
        public bool IsLatest { get
            {
                return !UpdateAvalaible();
            } }
        public GithubUpdater(string ProjectLink, Version MyFileVersion)
        {
            _ProjectLink = ProjectLink;
            CurrentVersion = MyFileVersion;
        }
        public bool UpdateAvalaible()
        {
            Version latest = GetUpdate().Result;
            return (latest > CurrentVersion);
        }
        private async Task<Version> GetUpdate()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("User-Agent", $"C# Update finder");
                    HttpResponseMessage response = await client.GetAsync($"{_ProjectLink}{LatestString}", HttpCompletionOption.ResponseHeadersRead);
                    string link = response.RequestMessage?.RequestUri?.ToString();
                    link = link.Replace(_ProjectLink, "").Replace("/releases/tag/", "").Replace("version-", "");
                    Version LatestDetected = Version.Parse(link);
                    return LatestDetected;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
