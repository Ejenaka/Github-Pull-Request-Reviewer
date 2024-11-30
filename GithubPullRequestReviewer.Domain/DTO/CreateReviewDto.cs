namespace GithubPullRequestReviewer.Domain.DTO
{
    public record CreateReviewDto
    {
        public long RepositoryId { get; set; }
        public int PullRequestNumber { get; set; }
        public List<CreateReviewDtoItem> Issues { get; set; }
        public List<CreateReviewDtoItem> Vulnerabilities { get; set; }
        public List<CreateReviewDtoItem> Optimizations { get; set; }
        public List<CreateReviewDtoItem> Enhancements { get; set; }
        public List<CreateReviewDtoItem> BestPractices { get; set; }
    }
}
