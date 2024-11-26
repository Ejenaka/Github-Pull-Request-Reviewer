using GithubPullRequestReviewer.DataAccess.Contracts;
using GithubPullRequestReviewer.DataAccess.Options;
using Microsoft.Extensions.Options;
using Octokit;

namespace GithubPullRequestReviewer.DataAccess.Services
{
    public class GithubTokenService : ITokenService
    {
        private readonly GitHubClient _githubClient;
        private readonly IOptions<GithubOAuthAppOptions> _options;

        public GithubTokenService(GitHubClient githubClient, IOptions<GithubOAuthAppOptions> options)
        {
            _githubClient = githubClient;
            _options = options;
        }

        public string GetToken()
        {
            return _githubClient.Credentials.GetToken();
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
