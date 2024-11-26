using Octokit;
using PullRequest = GithubPullRequestReviewer.Domain.Models.PullRequest;

namespace GithubPullRequestReviewer.BusinessLogic.Contracts
{
    public interface IPullRequestService
    {
        Task<PullRequest> GetPullRequestAsync(long repositoryId, int pullRequestNumber);
        Task<IList<PullRequest>> GetAllPullRequestsForRepositoryAsync(long repositoryId);
        Task<string> GetPullRequestDiffContentAsync(long repositoryId, int pullRequestNumber);
        Task CreatePullRequestAsync(string githubPullRequestId);
        Task<IEnumerable<PullRequestFile>> GetPullRequestFilesAsync(long repositoryId, int pullRequestNumber);
    }
}
