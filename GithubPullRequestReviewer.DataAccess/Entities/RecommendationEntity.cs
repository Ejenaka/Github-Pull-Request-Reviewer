using GithubPullRequestReviewer.Domain.Enums;

namespace GithubPullRequestReviewer.DataAccess.Entities
{
    public class RecommendationEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string FileName { get; set; }
        public int[]? CodeLines { get; set; }
        public long RepositoryId { get; set; }
        public int PullRequestNumber { get; set; }
        public RecommendationType Type { get; set; }
        public RecommendationStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<CommentEntity> Comments { get; set; }
    }
}
