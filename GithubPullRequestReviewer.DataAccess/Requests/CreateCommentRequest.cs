namespace GithubPullRequestReviewer.PullRequestAPI.Requests
{
    public record CreateCommentRequest(string Text, bool IsFromUser, int RecommendationId);
}
