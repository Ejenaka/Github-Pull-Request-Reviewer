namespace GithubPullRequestReviewer.DataAccess.Responses
{
    public class ReviewResultResponse
    {
        public List<ReviewResultResponseItem> Issues { get; set; }
        public List<ReviewResultResponseItem> Vulnerabilities { get; set; }
        public List<ReviewResultResponseItem> Optimization { get; set; }
        public List<ReviewResultResponseItem> Enhancements { get; set; }
        public List<ReviewResultResponseItem> BestPractices { get; set; }
    }
}
