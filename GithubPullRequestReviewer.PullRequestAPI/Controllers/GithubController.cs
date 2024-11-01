using GithubPullRequestReviewer.PullRequestAPI.Contracts;
using GithubPullRequestReviewer.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GithubPullRequestReviewer.PullRequestAPI.Controllers
{
    public class GithubController : Controller
    {
        private readonly IGithubProvider _githubProvider;

        public GithubController(IGithubProvider githubProvider)
        {
            _githubProvider = githubProvider;
        }

        [HttpGet]
        [Route("user")]
        [Authorize(AuthenticationSchemes = "GithuhUserAuthenticationScheme")]
        public async Task<User> GetUserInfo()
        {
            return await _githubProvider.GetUserInfoByTokenAsync();
        }

        [HttpGet]
        [Route("repositories")]
        [Authorize(AuthenticationSchemes = "GithuhUserAuthenticationScheme")]
        public async Task<IReadOnlyList<Repository>> GetUserRepositories()
        {
            return await _githubProvider.GetRepositoriesByTokenAsync();
        }
    }
}
