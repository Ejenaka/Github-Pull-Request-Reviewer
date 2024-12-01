using Octokit;

namespace GithubPullRequestReviewer.BusinessLogic.Contracts;

public interface IGithubWebhookService
{
    Task<IReadOnlyList<RepositoryHook>> GetAllRepositoryWebhooksAsync(long repositoryId, string accessToken);
    Task CreateWebhookAsync(long repositoryId, string accessToken);
}