using GithubPullRequestReviewer.Domain.Models;

namespace GithubPullRequestReviewer.PullRequestAPI.Contracts
{
    public interface IGithubProvider
    {
        Task<User> GetUserInfoByTokenAsync();
        Task<IReadOnlyList<Repository>> GetRepositoriesByTokenAsync();
    }
}
