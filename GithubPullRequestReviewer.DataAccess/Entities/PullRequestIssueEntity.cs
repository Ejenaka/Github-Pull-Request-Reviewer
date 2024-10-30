using GithubPullRequestReviewer.Domain.Enums;

namespace GithubPullRequestReviewer.DataAccess.Entities
{
    public class PullRequestIssueEntity
    {
        public int Id { get; set; }
        public int PullRequestId { get; set; }
        public int CommentId { get; set; }
        public int IssueTypeId { get; set; }

        public PullRequestEntity PullRequest { get; set; }
        public CommentEntity Comment { get; set; }
        public IssueType IssueType { get; set; }
    }
}
