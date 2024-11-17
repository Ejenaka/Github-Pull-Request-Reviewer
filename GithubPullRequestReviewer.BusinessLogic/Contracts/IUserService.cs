using GithubPullRequestReviewer.Domain.Models;

namespace GithubPullRequestReviewer.BusinessLogic.Contracts
{
    public interface IUserService
    {
        Task<User> GetCurrentUserAsync();
        string GetUserAuthLink();
        Task<string> GetUserAccessTokenAsync(string code);
    }
}