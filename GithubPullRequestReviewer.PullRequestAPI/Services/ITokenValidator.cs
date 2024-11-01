
namespace GithubPullRequestReviewer.PullRequestAPI.Services
{
    public interface ITokenValidator
    {
        Task<bool> ValidateAndSetTokenAsync(string token);
    }
}