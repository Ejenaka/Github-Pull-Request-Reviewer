using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.Domain.Models;
using GithubPullRequestReviewer.PullRequestAPI.Contracts;
using System.Text.Json;

namespace GithubPullRequestReviewer.BusinessLogic.Services
{
    public class PullRequestReviewer : IPullRequestReviewer
    {
        private readonly IGenerativeModelProvider _generativeModelProvider;
        private readonly IPullRequestService _pullRequestService;

        public PullRequestReviewer(IGenerativeModelProvider generativeModelProvider, IPullRequestService pullRequestService)
        {
            _generativeModelProvider = generativeModelProvider;
            _pullRequestService = pullRequestService;
        }

        public async Task<ReviewResult> ReviewPullRequestAsync(long repositoryId, int pullRequestNumber)
        {
            var pullRequestDetails = await _pullRequestService.GetPullRequestAsync(repositoryId, pullRequestNumber);
            var pullRequestDiffContent = await _pullRequestService.GetPullRequestDiffContentAsync(repositoryId, pullRequestNumber);

            var reviewRequestPrompt = PromptsForReview.BuildPromptForReview(pullRequestDetails.Name, pullRequestDiffContent);
            var modelResponse = await _generativeModelProvider.SendTextAsync(reviewRequestPrompt);

            var reviewResult = JsonSerializer.Deserialize<ReviewResult>(modelResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return reviewResult;
        }
    }
}
