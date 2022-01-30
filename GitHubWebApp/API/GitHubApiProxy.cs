using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GitHubWebApp.Models;
using Newtonsoft.Json;

namespace GitHubWebApp.API
{
    public class GitHubApiProxy
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public GitHubApiProxy(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GitHubUserModel> GetGitHubUserAsync(string username)
        {
            var client = CreateClient();
            client.BaseAddress = new Uri("https://api.github.com/users/");

            HttpResponseMessage response = await client.GetAsync(username);
            if (response.IsSuccessStatusCode)
            {
                GitHubUserModel user = await response.Content.ReadAsAsync<GitHubUserModel>();
                return user;
            }
            else
            {
                throw new WebException(response.ReasonPhrase);
            }
        }

        public async Task<List<RepoModel>> GetGitReposAsync(string repoUrl)
        {
            var client = CreateClient();
            HttpResponseMessage response = await client.GetAsync(repoUrl);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<RepoModel> allRepos = JsonConvert.DeserializeObject<List<RepoModel>>(json);

                return allRepos;
            }
            else
            {
                throw new WebException(response.ReasonPhrase);
            }
        }

        private HttpClient CreateClient()
        {
            return _httpClientFactory.CreateClient($"{nameof(GitHubApiProxy)}");
        }
    }
}
