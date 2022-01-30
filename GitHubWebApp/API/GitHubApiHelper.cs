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
    public class GitHubApiHelper
    {
        private readonly HttpClient _httpClient;
        public GitHubApiHelper()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("User-Agent","jacobtimms");
        }

        public async Task<GitHubUserModel> GetGitHubUserDetails(string username)
        {
            _httpClient.BaseAddress = new Uri("https://api.github.com/users/");

            HttpResponseMessage response = await _httpClient.GetAsync(username);
            if (response.IsSuccessStatusCode)
            {
                GitHubUserModel user = await response.Content.ReadAsAsync<GitHubUserModel>();
                List<Repo> allRepos = GetGitReposAsync(user.ReposUrl).Result;
                user.TopRepos = allRepos.OrderByDescending(x => x.StargazersCount).Take(5).ToArray();
                user.TotalStars = allRepos.Sum(repo => repo.StargazersCount);

                return user;
            }
            else
            {
                throw new WebException(response.ReasonPhrase);
            }
        }

        private async Task<List<Repo>> GetGitReposAsync(string repoUrl)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(repoUrl);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<Repo> allRepos = JsonConvert.DeserializeObject<List<Repo>>(json);
                
                return allRepos;
            }
            else
            {
                throw new WebException(response.ReasonPhrase);
            }
        }
    }
}
