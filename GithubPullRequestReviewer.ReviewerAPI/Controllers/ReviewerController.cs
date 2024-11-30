using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.DataAccess.Responses;
using GithubPullRequestReviewer.Domain.Models;
using GithubPullRequestReviewer.PullRequestAPI.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GithubPullRequestReviewer.ReviewerAPI.Controllers;

[ApiController]
[Route("api")]
public class ReviewerController : Controller
{
    private readonly IPullRequestReviewer _pullRequestReviewer;

    public ReviewerController(IPullRequestReviewer pullRequestReviewer)
    {
        _pullRequestReviewer = pullRequestReviewer;
    }

    [HttpGet]
    [Route("repositories/{repositoryId}/pull-requests/{pullRequestNumber}/review")]
    [Authorize(AuthenticationSchemes = "GithubUserAuthenticationScheme")]
    public async Task<ReviewResultResponse> ReviewPulRequestAsync(long repositoryId, int pullRequestNumber)
    {
        var reviewResult = await _pullRequestReviewer.ReviewPullRequestAsync(repositoryId, pullRequestNumber);
        return reviewResult;
    }

    [HttpPost]
    [Route("comments")]
    [Authorize(AuthenticationSchemes = "GithubUserAuthenticationScheme")]
    public async Task<Comment> AddCommentForRecommendationAndGetResponseAsync([FromBody] CreateCommentRequest createCommentRequest)
    {
        return await _pullRequestReviewer.AddCommentForRecommendationAndGetResponseAsync(createCommentRequest);
    }
}