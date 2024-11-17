using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.Domain.Models;

namespace GithubPullRequestReviewer.BusinessLogic.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly Octokit.GitHubClient _githubClient;

        public RepositoryService(Octokit.GitHubClient githubClient)
        {
            _githubClient = githubClient;
        }

        public async Task<IReadOnlyList<Repository>> GetRepositoriesForCurrentUserAsync()
        {
            var currentUser = await _githubClient.User.Current();
            var repositories = await _githubClient.Repository.GetAllForUser(currentUser.Login);

            return repositories.Select(x => x.ToDomain()).ToList();
        }
    }
}
