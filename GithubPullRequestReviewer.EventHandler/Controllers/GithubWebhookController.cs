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
    public async Task<IReadOnlyList<RepositoryHook>> GetAllRepositoryWebhooksAsync([FromHeader(Name = "Access_token")] string accessToken, long repositoryId)
    {
        return await _githubWebhookService.GetAllRepositoryWebhooksAsync(repositoryId, accessToken);
    }

    [HttpPost]
    [Route("github-webhooks")]
    public async Task CreateWebhookAsync([FromHeader(Name = "Access_token")] string accessToken, long repositoryId)
    {
        await _githubWebhookService.CreateWebhookAsync(repositoryId, accessToken);
    }
}