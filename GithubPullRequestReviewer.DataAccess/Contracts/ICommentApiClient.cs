using GithubPullRequestReviewer.Domain.Models;
using GithubPullRequestReviewer.PullRequestAPI.Requests;

namespace GithubPullRequestReviewer.DataAccess.Contracts;

public interface ICommentApiClient
{
    Task CreateCommentForRecommendationAsync(CreateCommentRequest createCommentRequest);
    Task<IList<Comment>> GetCommentsForRecommendationAsync(int recommendationId);
}