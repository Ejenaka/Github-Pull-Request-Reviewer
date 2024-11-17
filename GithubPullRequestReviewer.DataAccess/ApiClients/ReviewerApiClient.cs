using GithubPullRequestReviewer.DataAccess.Contracts;
using GithubPullRequestReviewer.DataAccess.Responses;

namespace GithubPullRequestReviewer.DataAccess.ApiClients;

public class ReviewerApiClient : BaseApiClient, IReviewerApiClient
{
    public ReviewerApiClient(string clientUrl) : base(clientUrl) { }

    public async Task<ReviewResultResponse> ReviewPulRequestAsync(long repositoryId, long pullRequestNumber)
    {
        var relativeUri = $"repositories/{repositoryId}/pull-requests/{pullRequestNumber}/review";
        return await RunGetRequestAsync<ReviewResultResponse>(relativeUri);
    }
}