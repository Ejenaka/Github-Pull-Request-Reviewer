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

            var recommendationsGrouped = recommendations
                .Select(r => r.ToDomain())
                .GroupBy(r => r.Type);

            return new ReviewResult
            {
                Issues = recommendationsGrouped.Where(x => x.Key == RecommendationType.Issue).FirstOrDefault()?.ToList(),
                Vulnerabilities = recommendationsGrouped.Where(x => x.Key == RecommendationType.Vulnerability).FirstOrDefault()?.ToList(),
                Optimization = recommendationsGrouped.Where(x => x.Key == RecommendationType.Optimization).FirstOrDefault()?.ToList(),
                Enhancements = recommendationsGrouped.Where(x => x.Key == RecommendationType.Enhancement).FirstOrDefault()?.ToList(),
                BestPractices = recommendationsGrouped.Where(x => x.Key == RecommendationType.BestPractice).FirstOrDefault()?.ToList(),
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
                .Concat(createReviewDto.Optimization.Select(x => new { Recomendation = x, Type = RecommendationType.Optimization }))
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
    }
}
