using GithubPullRequestReviewer.DataAccess.Contracts;
using GithubPullRequestReviewer.Domain.DTO;
using GithubPullRequestReviewer.Domain.Models;

namespace GithubPullRequestReviewer.DataAccess.ApiClients;

public class ReviewApiClient : BaseApiClient, IReviewApiClient
{
    public ReviewApiClient(string clientUrl, ITokenService tokenService) : base(clientUrl, tokenService) { }

    public async Task<ReviewResult> GetPullRequestReviewResultAsync(long repositoryId, int pullRequestNumber)
    {
        var relativeUrl = $"api/repositories/{repositoryId}/pull-requests/{pullRequestNumber}/review";
        return await RunGetRequestAsync<ReviewResult>(relativeUrl);
    }

    public async Task<Recommendation> GetRecommendationByIdAsync(int recommendationId)
    {
        var relativeUrl = $"api/recommendations/{recommendationId}";
        return await RunGetRequestAsync<Recommendation>(relativeUrl);
    }

    public async Task CreatePullRequestReviewAsync(CreateReviewDto createReviewDto)
    {
        var relativeUrl = "api/reviews";
        await RunPostRequestAsync(relativeUrl, createReviewDto);
    }
}