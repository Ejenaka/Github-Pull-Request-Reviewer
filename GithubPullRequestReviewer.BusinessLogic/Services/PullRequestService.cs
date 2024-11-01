using GithubPullRequestReviewer.PullRequestAPI.Contracts;
using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.Domain.Models;

namespace GithubPullRequestReviewer.PullRequestAPI.Services
{
    public class PullRequestService : IPullRequestService
    {
        private Octokit.GitHubClient _githubClient;

        public PullRequestService(Octokit.GitHubClient githubClient)
        {
            _githubClient = githubClient;
        }

        public async Task<PullRequest> GetPullRequestAsync(long repositoryId, int pullRequestNumber)
        {
            var pullRequest = await _githubClient.PullRequest.Get(repositoryId, pullRequestNumber);

            return pullRequest.ToDomain();
        }

        public async Task<IList<PullRequest>> GetAllPullRequestsForRepositoryAsync(long repositoryId)
        {
            var pullRequests = await _githubClient.PullRequest.GetAllForRepository(repositoryId);

            return pullRequests.Select(p => p.ToDomain()).ToList();
        }

        public async Task<string> GetPullRequestDiffContentAsync(long repositoryId, int pullRequestNumber)
        {
            var pullRequest = await GetPullRequestAsync(repositoryId, pullRequestNumber);

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(pullRequest.DiffUrl);
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

        public Task CreatePullRequestAsync(string githubPullRequestId)
        {
            throw new NotImplementedException();
        }
    }
}
