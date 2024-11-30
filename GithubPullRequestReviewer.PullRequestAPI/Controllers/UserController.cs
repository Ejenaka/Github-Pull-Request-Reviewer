using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.DataAccess.Requests;
using GithubPullRequestReviewer.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GithubPullRequestReviewer.PullRequestAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRepositoryService _repositoriesService;

        public UserController(IUserService userService, IRepositoryService repositoriesService)
        {
            _userService = userService;
            _repositoriesService = repositoriesService;
        }

        [HttpGet]
        [Route("current")]
        [Authorize(AuthenticationSchemes = "GithubUserAuthenticationScheme")]
        public async Task<User> GetUserInfo()
        {
            return await _userService.GetCurrentUserAsync();
        }

        [HttpGet]
        [Route("current/repositories")]
        [Authorize(AuthenticationSchemes = "GithubUserAuthenticationScheme")]
        public async Task<IReadOnlyList<Repository>> GetUserRepositoriesAsync()
        {
            return await _repositoriesService.GetRepositoriesForCurrentUserAsync();
        }

        [HttpPost]
        [Route("repositories/files")]
        [Authorize(AuthenticationSchemes = "GithubUserAuthenticationScheme")]
        public async Task<string> GetFileContentAsync([FromBody] GetFileContentRequest getFileContentRequestRequest)
        {
            return await _repositoriesService.GetFileContentAsync(
                getFileContentRequestRequest.RepositoryName,
                getFileContentRequestRequest.FilePath,
                getFileContentRequestRequest.HeadRef);
        }

        [HttpGet]
        [Route("auth/url")]
        public string GetLoginUrl()
        {
            return _userService.GetUserAuthLink();
        }

        [HttpGet]
        [Route("auth/token")]
        public async Task<string> GetAccessTokenAsync([FromQuery] string code)
        {
            return await _userService.GetUserAccessTokenAsync(code);
        }
    }
}
