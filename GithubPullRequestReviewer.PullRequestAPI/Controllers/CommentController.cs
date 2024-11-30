using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.DataAccess.Requests;
using GithubPullRequestReviewer.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GithubPullRequestReviewer.PullRequestAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [Route("repositories/{repositoryId}/pull-requests/{pullRequestNumber}/review/comments")]
        [Authorize(AuthenticationSchemes = "GithubUserAuthenticationScheme")]
        public async Task<IList<Comment>> GetCommentsForPullRequestAsync(long repositoryId, int pullRequestNumber)
        {
            return await _commentService.GetCommentsForPullRequestAsync(repositoryId, pullRequestNumber);
        }
        
        [HttpGet]
        [Route("recommendations/{recommendationId:int}/comments")]
        [Authorize(AuthenticationSchemes = "GithubUserAuthenticationScheme")]
        public async Task<IList<Comment>> GetCommentsForRecommendationAsync(int recommendationId)
        {
            return await _commentService.GetCommentsForRecommendationAsync(recommendationId);
        }

        [HttpPost]
        [Route("comments")]
        [Authorize(AuthenticationSchemes = "GithubUserAuthenticationScheme")]
        public async Task CreateCommentForRecommendationAsync([FromBody] CreateCommentRequest createCommentRequest)
        {
            await _commentService.CreateCommentForRecommendationAsync(
                createCommentRequest.Text,
                createCommentRequest.IsFromUser,
                createCommentRequest.RecommendationId);
        }


    }
}
