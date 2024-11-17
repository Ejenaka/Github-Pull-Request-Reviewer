using GithubPullRequestReviewer.DataAccess.Contracts;
using GithubPullRequestReviewer.Domain.DTO;

namespace GithubPullRequestReviewer.DataAccess.ApiClients;

public class ReviewApiClient : BaseApiClient, IReviewApiClient
{
    public ReviewApiClient(string clientUrl) : base(clientUrl) { }

    public async Task CreatePullRequestReviewAsync(CreateReviewDto createReviewDto)
    {
        var relativeUrl = "/reviews";
        await RunPostRequestAsync(relativeUrl, createReviewDto);
    }
}