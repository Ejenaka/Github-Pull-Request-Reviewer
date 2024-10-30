using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.Domain.Models;

namespace GithubPullRequestReviewer.BusinessLogic.Services
{
    public class GithubProvider : IGithubProvider
    {
        private readonly Octokit.GitHubClient _githubClient;

        public GithubProvider(Octokit.GitHubClient githubClient)
        {
            _githubClient = githubClient;
        }

        public async Task<User> GetUserInfoByTokenAsync(string accessToken)
        {
            ArgumentNullException.ThrowIfNull(accessToken);
            _githubClient.Credentials = new Octokit.Credentials(accessToken);

            var currentUser = await _githubClient.User.Current();

            return currentUser.ToDomain();
        }

        public async Task<IReadOnlyList<Repository>> GetRepositoriesByTokenAsync(string accessToken)
        {
            var currentUser = await GetUserInfoByTokenAsync(accessToken);
            var repositories = await _githubClient.Repository.GetAllForUser(currentUser.Username);

            return repositories.Select(x => x.ToDomain()).ToList();
        }
    }
}
