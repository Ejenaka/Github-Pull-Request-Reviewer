using GithubPullRequestReviewer.DataAccess.Requests;
using GithubPullRequestReviewer.Domain.Models;

namespace GithubPullRequestReviewer.DataAccess.Contracts;

public interface ICommentApiClient
{
    Task CreateCommentForRecommendationAsync(CreateCommentRequest createCommentRequest);
    Task<IList<Comment>> GetCommentsForRecommendationAsync(int recommendationId);
}