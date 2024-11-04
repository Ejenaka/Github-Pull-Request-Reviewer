using Microsoft.Extensions.Options;

namespace GithubPullRequestReviewer.BusinessLogic
{
    public class GithubOAuthAppOptions
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
    }
}
