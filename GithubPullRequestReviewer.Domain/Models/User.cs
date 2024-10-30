namespace GithubPullRequestReviewer.Domain.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Url { get; set; }
        public string AvatarUrl { get; set; }
    }
}
