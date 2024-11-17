using GithubPullRequestReviewer.Domain.Models;

namespace GithubPullRequestReviewer.DataAccess.Contracts;

public interface IPullRequestApiClient
{
    Task<PullRequest> GetPullRequestAsync(long repositoryId, int pullRequestNumber);
    Task<string> GetPullRequestDiffContentAsync(long repositoryId, int pullRequestNumber);
}