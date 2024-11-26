using GithubPullRequestReviewer.DataAccess.Contracts;
using GithubPullRequestReviewer.DataAccess.Responses;

namespace GithubPullRequestReviewer.DataAccess.ApiClients;

public class ReviewerApiClient : BaseApiClient, IReviewerApiClient
{
    public ReviewerApiClient(string clientUrl, ITokenService gitHubTokenService) : base(clientUrl, gitHubTokenService) { }

    public async Task<ReviewResultResponse> ReviewPulRequestAsync(long repositoryId, long pullRequestNumber)
    {
        var relativeUri = $"api/repositories/{repositoryId}/pull-requests/{pullRequestNumber}/review";
        return await RunGetRequestAsync<ReviewResultResponse>(relativeUri);
    }
}