using GithubPullRequestReviewer.DataAccess.Contracts;
using Octokit.Webhooks;
using Octokit.Webhooks.Events;
using Octokit.Webhooks.Events.PullRequest;

namespace GithubPullRequestReviewer.EventHandler.EventProcessors;

public class PullRequestWebhookEventProcessor : WebhookEventProcessor
{
    private readonly IReviewerApiClient _reviewerApiClient;

    public PullRequestWebhookEventProcessor(IReviewerApiClient reviewerApiClient)
    {
        _reviewerApiClient = reviewerApiClient;
    }

    protected override async Task ProcessPullRequestWebhookAsync(WebhookHeaders headers, PullRequestEvent pullRequestEvent, PullRequestAction action)
    {
        if (pullRequestEvent.Action == PullRequestAction.Opened || pullRequestEvent.Action == PullRequestAction.Synchronize)
        {
            await _reviewerApiClient.ReviewPulRequestAsync(pullRequestEvent.Repository.Id, pullRequestEvent.PullRequest.Number);
        }
    }
}