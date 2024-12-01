using GithubPullRequestReviewer.BusinessLogic.Contracts;
using Octokit;
using PullRequest = GithubPullRequestReviewer.Domain.Models.PullRequest;

namespace GithubPullRequestReviewer.BusinessLogic.Services
{
    public class PullRequestService : IPullRequestService
    {
        private Octokit.GitHubClient _githubClient;
        private IRepositoryService _repositoryService;

        public PullRequestService(Octokit.GitHubClient githubClient, IRepositoryService repositoryService)
        {
            _githubClient = githubClient;
            _repositoryService = repositoryService;
        }

        public async Task<PullRequest> GetPullRequestAsync(long repositoryId, int pullRequestNumber)
        {
            var pullRequest = await _githubClient.PullRequest.Get(repositoryId, pullRequestNumber);
            var repository = await _repositoryService.GetRepositoryById(repositoryId);
            
            return pullRequest.ToDomain(repository);
        }

        public async Task<IList<PullRequest>> GetAllPullRequestsForRepositoryAsync(long repositoryId)
        {
            var pullRequests = await _githubClient.PullRequest.GetAllForRepository(repositoryId);
            var repository = await _repositoryService.GetRepositoryById(repositoryId);

            return pullRequests.Select(p => p.ToDomain(repository)).ToList();
        }

        public async Task<string> GetPullRequestDiffContentAsync(long repositoryId, int pullRequestNumber)
        {
            var pullRequest = await GetPullRequestAsync(repositoryId, pullRequestNumber);

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(pullRequest.DiffUrl);
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

        public async Task<IEnumerable<PullRequestFile>> GetPullRequestFilesAsync(long repositoryId, int pullRequestNumber)
        {
            return await _githubClient.Repository.PullRequest.Files(repositoryId, pullRequestNumber);
        }
    }
}
