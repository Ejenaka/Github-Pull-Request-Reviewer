using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.DataAccess.Options;
using Microsoft.Extensions.Options;
using Octokit;

namespace GithubPullRequestReviewer.BusinessLogic.Services
{
    public class GithubTokenValidator : ITokenValidator
    {
        private readonly GitHubClient _githubClient;
        private readonly IOptions<GithubOAuthAppOptions> _options;

        public GithubTokenValidator(GitHubClient githubClient, IOptions<GithubOAuthAppOptions> options)
        {
            _githubClient = githubClient;
            _options = options;
        }

        public async Task<bool> ValidateAndSetTokenAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            _githubClient.Credentials = new Credentials(token);

            var currentUser = await _githubClient.User.Current();

            return currentUser != null;
        }
    }
}
