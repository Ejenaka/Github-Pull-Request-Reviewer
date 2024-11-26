using GithubPullRequestReviewer.Domain.DTO;
using GithubPullRequestReviewer.Domain.Models;

namespace GithubPullRequestReviewer.BusinessLogic.Contracts
{
    public interface IReviewService
    {
        Task CreatePullRequestReviewAsync(CreateReviewDto createReviewDto);
        Task<ReviewResult> GetPullRequestReviewResultAsync(long repositoryId, int pullRequestNumber);
        Task UpdatePullRequestRecommendationAsync(Recommendation recommendation);
        Task<Recommendation> GetRecommendationByIdAsync(int recommendationId);
        Task<IEnumerable<Recommendation>> GetRecommendationsAsync(long repositoryId, int pullRequestNumber);
    }
}
