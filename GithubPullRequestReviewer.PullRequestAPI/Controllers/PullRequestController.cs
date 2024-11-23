using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GithubPullRequestReviewer.PullRequestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PullRequestController : Controller
    {
        private readonly IPullRequestService _pullRequestService;

        public PullRequestController(IPullRequestService pullRequestService)
        {
            _pullRequestService = pullRequestService;
        }

        [HttpGet]
        [Route("repositories/{repositoryId}/pull-requests/{pullRequestNumber}")]
        [Authorize(AuthenticationSchemes = "GithubUserAuthenticationScheme")]
        public async Task<PullRequest> GetPullRequestAsync(long repositoryId, int pullRequestNumber)
        {
            return await _pullRequestService.GetPullRequestAsync(repositoryId, pullRequestNumber);
        }

        [HttpGet]
        [Route("repositories/{repositoryId}/pull-requests")]
        [Authorize(AuthenticationSchemes = "GithubUserAuthenticationScheme")]
        public async Task<IList<PullRequest>> GetAllPullRequestForRepositoriesAsync(long repositoryId)
        {
            return await _pullRequestService.GetAllPullRequestsForRepositoryAsync(repositoryId);
        }

        [HttpGet]
        [Route("repositories/{repositoryId}/pull-requests/{pullRequestNumber}/diff")]
        [Authorize(AuthenticationSchemes = "GithubUserAuthenticationScheme")]
        public async Task<string> GetPullRequestDiffContentAsync(long repositoryId, int pullRequestNumber)
        {
            return await _pullRequestService.GetPullRequestDiffContentAsync(repositoryId, pullRequestNumber);
        }
    }
}
