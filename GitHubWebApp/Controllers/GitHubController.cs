using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GitHubWebApp.Models;
using GitHubWebApp.API;

namespace GitHubWebApp.Controllers
{
    public class GitHubController : Controller
    {
        private readonly GitHubService _gitHubService;

        public GitHubController(GitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        public async Task<IActionResult> ShowUserAsync(SearchInputModel input)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var res = await _gitHubService.GetUserAcc(input.SearchInput);
                    return View(res);
                }
                catch (WebException)
                {
                    return View("~/Views/Shared/UserNotFoundException.cshtml");
                }
            }
            else
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }
    }
}
