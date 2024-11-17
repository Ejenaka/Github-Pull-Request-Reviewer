using GithubPullRequestReviewer.BusinessLogic.Contracts;
using Microsoft.Extensions.Configuration;
using Octokit;

namespace GithubPullRequestReviewer.BusinessLogic.Services;

public class GithubWebhookService : IGithubWebhookService
{
    private readonly Octokit.GitHubClient _client;
    private readonly IConfiguration _configuration;

    public GithubWebhookService(GitHubClient client, IConfiguration configuration)
    {
        _client = client;
        _configuration = configuration;
    }

    public async Task<IReadOnlyList<RepositoryHook>> GetAllRepositoryWebhooksAsync(long repositoryId)
    {
        return await _client.Repository.Hooks.GetAll(repositoryId);
    }

    public async Task CreateWebhookAsync(long repositoryId)
    {
        Dictionary<string, string> newWebhookConfig = new()
        {
            { "content_type", "json" },
            { "url", $"{_configuration.GetSection("ApiBaseUrls")["EventHandlerApi"]}/api/github/webhooks" },
            { "insecure_ssl", "0" },
        };

        string[] newWebhookEvents = ["pull_request"];

        var newWebhook = new NewRepositoryHook("web", newWebhookConfig)
        {
            Events = newWebhookEvents
        };

        await _client.Repository.Hooks.Create(repositoryId, newWebhook);
    }
}