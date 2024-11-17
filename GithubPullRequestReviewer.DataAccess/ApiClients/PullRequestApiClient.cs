using GithubPullRequestReviewer.DataAccess.Contracts;
using GithubPullRequestReviewer.Domain.Models;

namespace GithubPullRequestReviewer.DataAccess.ApiClients;

public class PullRequestApiClient : BaseApiClient, IPullRequestApiClient
{
    public PullRequestApiClient(string clientUrl) : base(clientUrl) { }

    public async Task<PullRequest> GetPullRequestAsync(long repositoryId, int pullRequestNumber)
    {
        var relativeUrl = $"repositories/{repositoryId}/pull-requests/{pullRequestNumber}";
        return await RunGetRequestAsync<PullRequest>(relativeUrl);
    }

    public async Task<string> GetPullRequestDiffContentAsync(long repositoryId, int pullRequestNumber)
    {
        var relativeUrl = $"repositories/{repositoryId}/pull-requests/{pullRequestNumber}/diff";
        return await RunGetRequestAsync<string>(relativeUrl);
    }
}