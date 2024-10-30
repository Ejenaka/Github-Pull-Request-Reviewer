namespace GithubPullRequestReviewer.DataAccess.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public string FileName { get; set; }
        public int[]? CodeLines { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public CommentEntity ParentComment { get; set; }
    }
}