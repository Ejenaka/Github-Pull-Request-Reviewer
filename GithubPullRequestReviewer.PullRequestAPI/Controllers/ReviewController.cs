using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.Domain.DTO;
using GithubPullRequestReviewer.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GithubPullRequestReviewer.PullRequestAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        [Route("repositories/{repositoryId}/pull-requests/{pullRequestNumber}/review")]
        [Authorize(AuthenticationSchemes = "GithubUserAuthenticationScheme")]

        public async Task<ReviewResult> GetPullRequestReviewResultAsync(long repositoryId, int pullRequestNumber)
        {
            return await _reviewService.GetPullRequestReviewResultAsync(repositoryId, pullRequestNumber);
        }

        [HttpPost]
        [Route("reviews")]
        [Authorize(AuthenticationSchemes = "GithubUserAuthenticationScheme")]
        public async Task CreatePullRequestReviewAsync([FromBody] CreateReviewDto createReviewDto)
        {
            await _reviewService.CreatePullRequestReviewAsync(createReviewDto);
        }

        [HttpPatch]
        [Route("reviews")]
        [Authorize(AuthenticationSchemes = "GithubUserAuthenticationScheme")]
        public async Task UpdatePullRequestRecommendationAsync([FromBody] Recommendation recommendation)
        {
            await _reviewService.UpdatePullRequestRecommendationAsync(recommendation);
        }
    }
}
