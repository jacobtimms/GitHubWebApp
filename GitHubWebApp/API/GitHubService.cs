using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GitHubWebApp.Models;

namespace GitHubWebApp.API
{
    public class GitHubService
    {
        private readonly GitHubApiProxy _gitHubApiProxy;

        public GitHubService(GitHubApiProxy gitHubApiProxy)
        {
            _gitHubApiProxy = gitHubApiProxy;
        }

        public async Task<GitHubUserModel> GetUserAcc(string username)
        {
            GitHubUserModel user = await _gitHubApiProxy.GetGitHubUserAsync(username);
            List<RepoModel> allRepos = _gitHubApiProxy.GetGitReposAsync(user.ReposUrl).Result;

            user.TopRepos = allRepos.OrderByDescending(x => x.StargazersCount).Take(5).ToArray();
            user.TotalStars = allRepos.Sum(repo => repo.StargazersCount);

            return user;
        }
    }
}
