using GithubPullRequestReviewer.Domain.Enums;

namespace GithubPullRequestReviewer.DataAccess.Entities
{
    public class PullRequestEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int StatusId { get; set; }
        public int RepositoryId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public PullRequestStatus Status { get; set; }
        public RepositoryEntity Repository { get; set; }
        public List<CommentEntity> Comments { get; set; }
    }
}
