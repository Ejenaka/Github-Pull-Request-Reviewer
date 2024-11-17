using GithubPullRequestReviewer.DataAccess.Responses;
using GithubPullRequestReviewer.Domain.Models;
using GithubPullRequestReviewer.PullRequestAPI.Requests;

namespace GithubPullRequestReviewer.BusinessLogic.Contracts
{
    public interface IPullRequestReviewer
    {
        Task<ReviewResultResponse> ReviewPullRequestAsync(long repositoryId, int pullRequestNumber);
        Task<Comment> AddCommentForRecommendationAndGetResponseAsync(CreateCommentRequest createCommentRequest);
    }
}