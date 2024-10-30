using GithubPullRequestReviewer.Domain.Models;

namespace GithubPullRequestReviewer.BusinessLogic.Contracts
{
    public interface IGithubProvider
    {
        Task<User> GetUserInfoByTokenAsync(string accessToken);
        Task<IReadOnlyList<Repository>> GetRepositoriesByTokenAsync(string accessToken);
    }
}
