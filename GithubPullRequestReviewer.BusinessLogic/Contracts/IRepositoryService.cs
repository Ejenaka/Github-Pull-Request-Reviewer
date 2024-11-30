using GithubPullRequestReviewer.Domain.Models;

namespace GithubPullRequestReviewer.BusinessLogic.Contracts
{
    public interface IRepositoryService
    {
        Task<IReadOnlyList<Repository>> GetRepositoriesForCurrentUserAsync();
        Task<Repository> GetRepositoryById(long repositoryId);
        Task<string> GetFileContentAsync(string repositoryName, string filePath, string headRef);
    }
}