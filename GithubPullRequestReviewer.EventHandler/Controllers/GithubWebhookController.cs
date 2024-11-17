using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Octokit;

namespace GithubPullRequestReviewer.EventHandler.Controllers;

[ApiController]
[Route("api")]
public class GithubWebhookController : Controller
{
    private readonly IGithubWebhookService _githubWebhookService;

    public GithubWebhookController(IGithubWebhookService githubWebhookService)
    {
        _githubWebhookService = githubWebhookService;
    }

    [HttpGet]
    [Route("repositories/{repositoryId}/github-webhooks")]
    [Authorize(AuthenticationSchemes = "GithubUserAuthenticationScheme")]
    public async Task<IReadOnlyList<RepositoryHook>> GetAllRepositoryWebhooksAsync(long repositoryId)
    {
        return await _githubWebhookService.GetAllRepositoryWebhooksAsync(repositoryId);
    }

    [HttpPost]
    [Route("github-webhooks")]
    [Authorize(AuthenticationSchemes = "GithubUserAuthenticationScheme")]
    public async Task CreateWebhookAsync(long repositoryId)
    {
        await _githubWebhookService.CreateWebhookAsync(repositoryId);
    }
}