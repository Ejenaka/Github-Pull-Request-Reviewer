using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.DataAccess.Options;
using Microsoft.Extensions.Options;
using Octokit;
using User = GithubPullRequestReviewer.Domain.Models.User;

namespace GithubPullRequestReviewer.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly Octokit.GitHubClient _githubClient;
        private readonly IOptions<GithubOAuthAppOptions> _githubOAuthOptions;

        public UserService(Octokit.GitHubClient githubClient, IOptions<GithubOAuthAppOptions> githubOAuthOptions)
        {
            _githubClient = githubClient;
            _githubOAuthOptions = githubOAuthOptions;
        }

        public async Task<User> GetCurrentUserAsync()
        {
            var currentUser = await _githubClient.User.Current();
            return currentUser.ToDomain();
        }

        public string GetUserAuthLink()
        {
            var authRequest = new OauthLoginRequest(_githubOAuthOptions.Value.ClientId)
            {
                Scopes = { "user", "repo" }
            };
            
            return _githubClient.Oauth.GetGitHubLoginUrl(authRequest).ToString();
        }

        public async Task<string> GetUserAccessTokenAsync(string code)
        {
            var token = await _githubClient.Oauth.CreateAccessToken(new OauthTokenRequest(
                _githubOAuthOptions.Value.ClientId,
                _githubOAuthOptions.Value.Secret,
                code));
            
            return token.AccessToken;
        }
    }
}
