using Microsoft.Extensions.Options;

namespace GithubPullRequestReviewer.PullRequestAPI.Configuration
{
    public class GithubOAuthAppOptions
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
    }
}
