using Octokit.Webhooks;
using Octokit.Webhooks.Events;
using Octokit.Webhooks.Events.PullRequest;

namespace GithubPullRequestReviewer.EventHandler.EventProcessors
{
    public class PullRequestWebhookEventProcessor : WebhookEventProcessor
    {
        protected override Task ProcessPullRequestWebhookAsync(WebhookHeaders headers, PullRequestEvent pullRequestEvent, PullRequestAction action)
        {
            return base.ProcessPullRequestWebhookAsync(headers, pullRequestEvent, action);
        }
    }
}
