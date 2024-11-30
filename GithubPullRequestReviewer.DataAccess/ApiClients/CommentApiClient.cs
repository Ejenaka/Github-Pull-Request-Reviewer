using GithubPullRequestReviewer.DataAccess.Contracts;
using GithubPullRequestReviewer.Domain.Models;
using GithubPullRequestReviewer.PullRequestAPI.Requests;

namespace GithubPullRequestReviewer.DataAccess.ApiClients
{
    public class CommentApiClient : BaseApiClient, ICommentApiClient
    {
        public CommentApiClient(string clientUrl, ITokenService tokenService) : base(clientUrl, tokenService) { }

        public async Task<IList<Comment>> GetCommentsForRecommendationAsync(int recommendationId)
        {
            var relativeUrl = $"api/recommendations/{recommendationId}/comments";
            return await RunGetRequestAsync<List<Comment>>(relativeUrl);
        }
        
        public async Task CreateCommentForRecommendationAsync(CreateCommentRequest createCommentRequest)
        {
            var relativeUrl = "api/comments";
            await RunPostRequestAsync(relativeUrl, createCommentRequest);
        }
    }
}
