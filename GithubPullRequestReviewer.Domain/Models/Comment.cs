namespace GithubPullRequestReviewer.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public string FileName { get; set; }
        public int[]? CodeLines { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public Comment ParentComment { get; set; }
    }
}