using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GithubPullRequestReviewer.ReviewerAPI.Controllers
{
    [ApiController]
    [Route("[controller]/api")]
    public class ReviewerController : Controller
    {
        private readonly IPullRequestReviewer _pullRequestReviewer;

        public ReviewerController(IPullRequestReviewer pullRequestReviewer)
        {
            _pullRequestReviewer = pullRequestReviewer;
        }

        [HttpGet]
        [Route("repositories/{repositoryId}/pull-requests/{pullRequestNumber}/review")]
        [Authorize(AuthenticationSchemes = "GithuhUserAuthenticationScheme")]
        public async Task<ReviewResult> ReviewPulRequestAsync(long repositoryId, int pullRequestNumber)
        {
            var reviewResult = await _pullRequestReviewer.ReviewPullRequestAsync(repositoryId, pullRequestNumber);

            return reviewResult;
        }
    }
}
