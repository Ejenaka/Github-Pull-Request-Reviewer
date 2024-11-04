namespace GithubPullRequestReviewer.Domain.Models
{
    public class ReviewResult
    {
        public List<ReviewResultItem> Issues { get; set; }
        public List<ReviewResultItem> Vulnerabilities { get; set; }
        public List<ReviewResultItem> Optimization { get; set; }
        public List<ReviewResultItem> Enhancements { get; set; }
        public List<ReviewResultItem> BestPractices { get; set; }
    }
}
