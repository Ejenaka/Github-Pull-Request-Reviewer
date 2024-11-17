namespace GithubPullRequestReviewer.DataAccess.Responses
{
    public class ReviewResultResponseItem
    {
        public string Description { get; set; }
        public string File { get; set; }
        public int BeginsAtCodeLine { get; set; }
        public int EndsAtCodeLine { get; set; }
    }
}
