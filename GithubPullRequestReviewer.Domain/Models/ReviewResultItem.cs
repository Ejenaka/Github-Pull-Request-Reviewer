namespace GithubPullRequestReviewer.Domain.Models
{
    public class ReviewResultItem
    {
        public string Description { get; set; }
        public string File { get; set; }
        public int BeginsAtCodeLine { get; set; }
        public int EndsAtCodeLine { get; set; }
    }
}
