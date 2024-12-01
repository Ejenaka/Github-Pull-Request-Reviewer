using GithubPullRequestReviewer.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace GithubPullRequestReviewer.DataAccess
{
    public class PullRequestReviewerDbContext : DbContext
    {
        public DbSet<RecommendationEntity> Recommendations { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }

        public PullRequestReviewerDbContext() { }
        
        public PullRequestReviewerDbContext(DbContextOptions<PullRequestReviewerDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost:5433;Username=postgres;Password=qwerty;Database=GithubPullRequestReviewer");
        }
    }
}
