namespace GithubPullRequestReviewer.DataAccess.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public long UserId { get; set; }
        public int ParentCommentId { get; set; }
        public int PullRequestIssueId { get; set; }
        public DateTime CreatedAt { get; set; }

        public CommentEntity ParentComment { get; set; }
        public PullRequestIssueEntity PullRequestIssue { get; set; }
    }
}