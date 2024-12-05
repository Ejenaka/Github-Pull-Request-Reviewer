using GithubPullRequestReviewer.BusinessLogic.Contracts;
using Microsoft.Extensions.Configuration;
using Octokit;

namespace GithubPullRequestReviewer.BusinessLogic.Services;

public class GithubWebhookService : IGithubWebhookService
{
    private readonly Octokit.GitHubClient _client;
    private readonly string _hookUrl;

    public GithubWebhookService(GitHubClient client, IConfiguration configuration)
    {
        _client = client;
        _hookUrl = $"{configuration.GetSection("ApiBaseUrls")["EventHandlerApi"]}/api/github/webhooks";
    }

    public async Task<IReadOnlyList<RepositoryHook>> GetAllRepositoryWebhooksAsync(long repositoryId, string accessToken)
    {
        _client.Credentials = new Credentials(accessToken);
        return await _client.Repository.Hooks.GetAll(repositoryId);
    }

    public async Task CreateWebhookAsync(long repositoryId, string accessToken)
    {
        _client.Credentials = new Credentials(accessToken);
        Dictionary<string, string> newWebhookConfig = new()
        {
            { "content_type", "json" },
            { "url", _hookUrl },
            { "insecure_ssl", "0" },
        };

        string[] newWebhookEvents = ["pull_request"];

        var newWebhook = new NewRepositoryHook("web", newWebhookConfig)
        {
            Events = newWebhookEvents
        };

        await _client.Repository.Hooks.Create(repositoryId, newWebhook);
    }

    public async Task DeleteWebhookAsync(long repositoryId, string accessToken)
    {
        _client.Credentials = new Credentials(accessToken);

        var webhooks = await _client.Repository.Hooks.GetAll(repositoryId);
        var hookToDelete = webhooks.First(h => h.Url == _hookUrl);
        
        await _client.Repository.Hooks.Delete(repositoryId, hookToDelete.Id);
    }
}