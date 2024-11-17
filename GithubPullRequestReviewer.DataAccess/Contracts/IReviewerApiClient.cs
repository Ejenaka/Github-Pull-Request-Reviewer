using GithubPullRequestReviewer.DataAccess.ApiClients;
using GithubPullRequestReviewer.DataAccess.Responses;

namespace GithubPullRequestReviewer.DataAccess.Contracts;

public interface IReviewerApiClient
{
    Task<ReviewResultResponse> ReviewPulRequestAsync(long repositoryId, long pullRequestNumber);
}