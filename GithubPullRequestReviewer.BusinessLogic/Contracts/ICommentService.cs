using GithubPullRequestReviewer.Domain.Models;

namespace GithubPullRequestReviewer.BusinessLogic.Contracts
{
    public interface ICommentService
    {
        Task CreateCommentForRecommendationAsync(string text, bool isFromUser, int recommendationId);
        Task<IList<Comment>> GetCommentsForPullRequestAsync(long repositoryId, int pullRequestNumber);
        Task<IList<Comment>> GetCommentsForRecommendationAsync(int recommendationId);
    }
}