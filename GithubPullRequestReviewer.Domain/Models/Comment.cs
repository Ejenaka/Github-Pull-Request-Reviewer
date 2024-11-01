namespace GithubPullRequestReviewer.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public long UserId { get; set; }
        public int ParentCommentId { get; set; }
        public int PullRequestIssueId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Comment ParentComment { get; set; }
    }
}