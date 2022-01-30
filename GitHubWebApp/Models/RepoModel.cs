using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GitHubWebApp.Models
{
    public class RepoModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("html_url")]
        public string Url { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("stargazers_count")]
        public int StargazersCount { get; set; }
    }
}
