using GithubPullRequestReviewer.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace GithubPullRequestReviewer.DataAccess
{
    public class PullRequestReviewerDbContext : DbContext
    {
        public DbSet<PullRequestEntity> PullRequests { get; set; }
        public DbSet<PullRequestIssueEntity> PullRequestIssues { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<RepositoryEntity> Repositories { get; set; }

        public PullRequestReviewerDbContext() : base() { }
        public PullRequestReviewerDbContext(DbContextOptions<PullRequestReviewerDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost:5432;Username=postgres;Password=qwerty;Database=GithubPullRequestReviewer");
        }
    }
}
