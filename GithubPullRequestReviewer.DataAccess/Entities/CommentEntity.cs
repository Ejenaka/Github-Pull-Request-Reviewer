namespace GithubPullRequestReviewer.DataAccess.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }
        public int RecommendationId { get; set; }
        public string Text { get; set; }
        public bool IsFromUser { get; set; }
        public DateTime CreatedAt { get; set; }

        public RecommendationEntity Recommendation { get; set; }
    }
}