using Octokit;

namespace GithubPullRequestReviewer.BusinessLogic.Contracts;

public interface IGithubWebhookService
{
    Task<IReadOnlyList<RepositoryHook>> GetAllRepositoryWebhooksAsync(long repositoryId);
    Task CreateWebhookAsync(long repositoryId);
}