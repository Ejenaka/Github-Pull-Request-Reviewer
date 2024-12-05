using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.DataAccess;
using GithubPullRequestReviewer.DataAccess.Entities;
using GithubPullRequestReviewer.Domain.DTO;
using GithubPullRequestReviewer.Domain.Enums;
using GithubPullRequestReviewer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GithubPullRequestReviewer.BusinessLogic.Services
{
    public class ReviewService : IReviewService
    {
        private readonly PullRequestReviewerDbContext _dbContext;

        public ReviewService(PullRequestReviewerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ReviewResult> GetPullRequestReviewResultAsync(long repositoryId, int pullRequestNumber)
        {
            var recommendations = await _dbContext.Recommendations
                .Where(r => r.RepositoryId == repositoryId && r.PullRequestNumber == pullRequestNumber)
                .ToListAsync();

            if (recommendations.Count == 0)
            {
                return new ReviewResult();
            }

            var recommendationsGrouped = recommendations
                .Select(r => r.ToDomain())
                .GroupBy(r => r.Type);

            return new ReviewResult
            {
                Issues = recommendationsGrouped.FirstOrDefault(x => x.Key == RecommendationType.Issue)?.ToList(),
                Vulnerabilities = recommendationsGrouped.FirstOrDefault(x => x.Key == RecommendationType.Vulnerability)?.ToList(),
                Optimization = recommendationsGrouped.FirstOrDefault(x => x.Key == RecommendationType.Optimization)?.ToList(),
                Enhancements = recommendationsGrouped.FirstOrDefault(x => x.Key == RecommendationType.Enhancement)?.ToList(),
                BestPractices = recommendationsGrouped.FirstOrDefault(x => x.Key == RecommendationType.BestPractice)?.ToList(),
            };
        }

        public async Task CreatePullRequestReviewAsync(CreateReviewDto createReviewDto)
        {
            createReviewDto.Issues ??= [];
            createReviewDto.Vulnerabilities ??= [];
            createReviewDto.BestPractices ??= [];
            createReviewDto.Enhancements ??= [];

            var recommendations = createReviewDto.Issues
                .Select(x => new { Recomendation = x, Type = RecommendationType.Issue })
                .Concat(createReviewDto.Vulnerabilities.Select(x => new { Recomendation = x, Type = RecommendationType.Vulnerability }))
                .Concat(createReviewDto.Enhancements.Select(x => new { Recomendation = x, Type = RecommendationType.Enhancement }))
                .Concat(createReviewDto.Optimizations.Select(x => new { Recomendation = x, Type = RecommendationType.Optimization }))
                .Concat(createReviewDto.BestPractices.Select(x => new { Recomendation = x, Type = RecommendationType.BestPractice }));

            var recommendationsDb = recommendations.Select(x => new RecommendationEntity
            {
                RepositoryId = createReviewDto.RepositoryId,
                PullRequestNumber = createReviewDto.PullRequestNumber,
                CodeLines = [x.Recomendation.BeginsAtCodeLine, x.Recomendation.EndsAtCodeLine],
                Content = x.Recomendation.Description,
                FileName = x.Recomendation.File,
                Type = x.Type,
                Status = RecommendationStatus.Open,
                CreatedAt = DateTime.Now,
            });

            await _dbContext.AddRangeAsync(recommendationsDb);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePullRequestRecommendationAsync(Recommendation recommendation)
        {
            _dbContext.Update(recommendation.ToDb());
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Recommendation> GetRecommendationByIdAsync(int recommendationId)
        {
            var recommendation = await _dbContext.Recommendations.FirstOrDefaultAsync(r => r.Id == recommendationId);
            
            return recommendation.ToDomain();
        }

        public async Task<IEnumerable<Recommendation>> GetRecommendationsAsync(long repositoryId, int pullRequestNumber)
        {
            var recommendations = await _dbContext.Recommendations
                .Where(r => r.RepositoryId == repositoryId && r.PullRequestNumber == pullRequestNumber)
                .ToListAsync();

            return recommendations.Select(r => r.ToDomain());
        }
    }
}
