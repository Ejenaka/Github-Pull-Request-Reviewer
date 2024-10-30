namespace GithubPullRequestReviewer.Domain.Models
{
    public class Repository
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public User Owner { get; set; }
    }
}