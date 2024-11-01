using GithubPullRequestReviewer.PullRequestAPI.Contracts;
using GithubPullRequestReviewer.Domain.Models;

namespace GithubPullRequestReviewer.PullRequestAPI.Services
{
    public class GithubProvider : IGithubProvider
    {
        private readonly Octokit.GitHubClient _githubClient;

        public GithubProvider(Octokit.GitHubClient githubClient)
        {
            _githubClient = githubClient;
        }

        public async Task<User> GetUserInfoByTokenAsync()
        {
            var currentUser = await _githubClient.User.Current();

            return currentUser.ToDomain();
        }

        public async Task<IReadOnlyList<Repository>> GetRepositoriesByTokenAsync()
        {
            var currentUser = await GetUserInfoByTokenAsync();
            var repositories = await _githubClient.Repository.GetAllForUser(currentUser.Username);

            return repositories.Select(x => x.ToDomain()).ToList();
        }
    }
}
