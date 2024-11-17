using GithubPullRequestReviewer.Domain.DTO;

namespace GithubPullRequestReviewer.DataAccess.Contracts;

public interface IReviewApiClient
{
    Task CreatePullRequestReviewAsync(CreateReviewDto createReviewDto);
}