namespace GithubPullRequestReviewer.Domain.DTO
{
    public record CreateReviewDtoItem
    {
        public string Description { get; set; }
        public string File { get; set; }
        public int BeginsAtCodeLine { get; set; }
        public int EndsAtCodeLine { get; set; }
    }
}
