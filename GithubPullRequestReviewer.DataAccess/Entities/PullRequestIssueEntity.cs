using GithubPullRequestReviewer.Domain.Enums;

namespace GithubPullRequestReviewer.DataAccess.Entities
{
    public class PullRequestIssueEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string FileName { get; set; }
        public int[]? CodeLines { get; set; }
        public long PullRequestId { get; set; }
        public IssueType IssueType { get; set; }
        public IssueStatus IssueStatus { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<CommentEntity> Comments { get; set; }
    }
}
