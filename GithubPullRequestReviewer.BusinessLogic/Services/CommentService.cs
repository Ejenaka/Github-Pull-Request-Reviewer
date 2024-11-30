using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.DataAccess;
using GithubPullRequestReviewer.DataAccess.Entities;
using GithubPullRequestReviewer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GithubPullRequestReviewer.BusinessLogic.Services;

public class CommentService : ICommentService
{
    private readonly PullRequestReviewerDbContext _dbContext;

    public CommentService(PullRequestReviewerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<Comment>> GetCommentsForPullRequestAsync(long repositoryId, int pullRequestNumber)
    {
        var commentsDb = await _dbContext.Comments
            .Include(x => x.Recommendation)
            .Where(c => c.Recommendation.RepositoryId == repositoryId && c.Recommendation.PullRequestNumber == pullRequestNumber)
            .ToListAsync();

        var comments = commentsDb.Select(c => c.ToDomain()).ToList();

        return comments;
    }

    public async Task<IList<Comment>> GetCommentsForRecommendationAsync(int recommendationId)
    {
        var commentsDb = await _dbContext.Comments
            .Where(c => c.RecommendationId == recommendationId)
            .ToListAsync();

        return commentsDb.Select(c => c.ToDomain()).ToList();
    }

    public async Task CreateCommentForRecommendationAsync(string text, bool isFromUser, int recommendationId)
    {
        var commentDb = new CommentEntity
        {
            Text = text,
            IsFromUser = isFromUser,
            RecommendationId = recommendationId,
            CreatedAt = DateTime.Now
        };

        await _dbContext.Comments.AddAsync(commentDb);
        await _dbContext.SaveChangesAsync();
    }
}