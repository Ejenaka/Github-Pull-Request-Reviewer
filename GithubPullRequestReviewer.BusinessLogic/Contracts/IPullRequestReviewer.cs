using GithubPullRequestReviewer.Domain.Models;

namespace GithubPullRequestReviewer.BusinessLogic.Contracts
{
    public interface IPullRequestReviewer
    {
        Task<ReviewResult> ReviewPullRequestAsync(long repositoryId, int pullRequestNumber);
    }
}