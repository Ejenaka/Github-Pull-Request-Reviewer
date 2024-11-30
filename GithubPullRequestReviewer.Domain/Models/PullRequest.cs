using GithubPullRequestReviewer.Domain.Enums;

namespace GithubPullRequestReviewer.Domain.Models
{
    public class PullRequest
    {
        public long Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string HeadRef { get; set; }
        public string DiffUrl { get; set; }
        public PullRequestStatus Status { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public User Creator { get; set; }
        public Repository Repository { get; set; }
    }
}
