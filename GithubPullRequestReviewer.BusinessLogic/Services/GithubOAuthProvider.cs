using Microsoft.Extensions.Options;
using Octokit;

namespace GithubPullRequestReviewer.BusinessLogic.Services
{
    public class GithubOAuthProvider
    {
        private readonly IOptions<GithubOAuthAppOptions> _options;
        private readonly GitHubClient _githubClient;

        public GithubOAuthProvider(IOptions<GithubOAuthAppOptions> options, GitHubClient githubClient)
        {
            _options = options;
            _githubClient = githubClient;
        }

        public string GetGithubLoginUrl()
        {
            var loginRequest = new OauthLoginRequest(_options.Value.ClientId)
            {
                Scopes = { "user", },
            };

            return _githubClient.Oauth.GetGitHubLoginUrl(loginRequest).ToString();
        }
    }
}
