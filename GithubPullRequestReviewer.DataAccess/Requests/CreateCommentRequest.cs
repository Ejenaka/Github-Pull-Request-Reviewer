namespace GithubPullRequestReviewer.DataAccess.Requests
{
    public record CreateCommentRequest(string Text, bool IsFromUser, int RecommendationId);
}
