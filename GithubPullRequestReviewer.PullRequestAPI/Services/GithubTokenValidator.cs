﻿using GithubPullRequestReviewer.PullRequestAPI.Configuration;
using Microsoft.Extensions.Options;
using Octokit;

namespace GithubPullRequestReviewer.PullRequestAPI.Services
{
    public class GithubTokenValidator : ITokenValidator
    {
        private readonly Octokit.GitHubClient _githubClient;
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
