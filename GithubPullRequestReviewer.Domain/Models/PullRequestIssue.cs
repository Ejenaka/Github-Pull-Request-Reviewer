using GithubPullRequestReviewer.Domain.Enums;

namespace GithubPullRequestReviewer.Domain.Models
{
    public class PullRequestIssue
    {
        public int Id { get; set; }
        public int PullRequestId { get; set; }
        public int CommentId { get; set; }
        public int IssueTypeId { get; set; }

        public PullRequest PullRequest { get; set; }
        public Comment Comment { get; set; }
        public IssueType IssueType { get; set; }
    }
}
