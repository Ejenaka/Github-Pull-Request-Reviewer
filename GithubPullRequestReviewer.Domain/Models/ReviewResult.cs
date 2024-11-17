namespace GithubPullRequestReviewer.Domain.Models
{
    public class ReviewResult
    {
        public List<Recommendation> Issues { get; set; }
        public List<Recommendation> Vulnerabilities { get; set; }
        public List<Recommendation> Optimization { get; set; }
        public List<Recommendation> Enhancements { get; set; }
        public List<Recommendation> BestPractices { get; set; }
    }
}
