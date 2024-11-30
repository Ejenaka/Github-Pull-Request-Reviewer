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

        public async Task<Repository> GetRepositoryById(long repositoryId)
        {
            var repository = await _githubClient.Repository.Get(repositoryId);

            return repository.ToDomain();
        }
        
        public async Task<string> GetFileContentAsync(string repositoryName, string filePath, string headRef)
        {
            var currentUser = await _githubClient.User.Current();
            var fileContent = await _githubClient.Repository.Content.GetRawContentByRef(
                currentUser.Login,
                repositoryName,
                filePath,
                headRef);
            
            return Convert.ToBase64String(fileContent);
        }
    }
}
