using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.Domain.Models;
using Microsoft.AspNetCore.Mvc;

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
        [Route("user-info")]
        public async Task<User> GetUserInfo([FromHeader] string accessToken)
        {
            return await _githubProvider.GetUserInfoByTokenAsync(accessToken);
        }

        [HttpGet]
        [Route("repositories")]
        public async Task<IReadOnlyList<Repository>> GetUserRepositories([FromHeader] string accessToken)
        {
            return await _githubProvider.GetRepositoriesByTokenAsync(accessToken);
        }
    }
}
