using GithubPullRequestReviewer.DataAccess.Contracts;
using Octokit;

namespace GithubPullRequestReviewer.DataAccess.Services
{
    public class GithubTokenService : ITokenService
    {
        private readonly GitHubClient _githubClient;

        public GithubTokenService(GitHubClient githubClient)
        {
            _githubClient = githubClient;
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
