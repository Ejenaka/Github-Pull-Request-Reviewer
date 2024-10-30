using GithubPullRequestReviewer.Domain.Enums;

namespace GithubPullRequestReviewer.Domain.Models
{
    public class PullRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int StatusId { get; set; }
        public int RepositoryId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public PullRequestStatus Status { get; set; }
        public Repository Repository { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
