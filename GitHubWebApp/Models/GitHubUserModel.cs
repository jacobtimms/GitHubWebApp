using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GitHubWebApp.Models
{
    public class GitHubUserModel
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("repos_url")]
        public string ReposUrl { get; set; }

        public int TotalStars { get; set; }

        public Repo[] TopRepos { get; set; }
    }
}
