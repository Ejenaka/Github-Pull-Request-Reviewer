using GithubPullRequestReviewer.Domain.DTO;
using GithubPullRequestReviewer.Domain.Models;

namespace GithubPullRequestReviewer.DataAccess.Contracts;

public interface IReviewApiClient
{
    Task CreatePullRequestReviewAsync(CreateReviewDto createReviewDto);
    Task<ReviewResult> GetPullRequestReviewResultAsync(long repositoryId, int pullRequestNumber);
    Task<Recommendation> GetRecommendationByIdAsync(int recommendationId);
}