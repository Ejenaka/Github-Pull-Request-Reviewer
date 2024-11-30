using GithubPullRequestReviewer.DataAccess.Entities;
using GithubPullRequestReviewer.DataAccess.Responses;
using GithubPullRequestReviewer.Domain.DTO;
using GithubPullRequestReviewer.Domain.Enums;
using GithubPullRequestReviewer.Domain.Models;

namespace GithubPullRequestReviewer.BusinessLogic
{
    public static class MapperExtension
    {
        public static PullRequest ToDomain(this Octokit.PullRequest pullRequest, Repository repository) =>
            new PullRequest
            {
                Id = pullRequest.Id,
                Number = pullRequest.Number,
                Name = pullRequest.Title,
                Status = (PullRequestStatus)pullRequest.State.Value,
                LastModifiedDate = pullRequest.UpdatedAt.DateTime,
                DiffUrl = pullRequest.DiffUrl,
                HeadRef = pullRequest.Head.Ref,
                Creator = pullRequest.User.ToDomain(),
                Repository = repository,
            };

        public static Repository ToDomain(this Octokit.Repository repository) =>
            new Repository
            {
                Id = repository.Id,
                Name = repository.Name,
                Url = repository.Url,
                Owner = repository.Owner.ToDomain(),
            };

        public static User ToDomain(this Octokit.User user) =>
            new User
            {
                Id = user.Id,
                Username = user.Login,
                AvatarUrl = user.AvatarUrl,
                Url = user.Url,
            };

        public static Recommendation ToDomain(this RecommendationEntity recommendation) =>
            new Recommendation
            {
                Id = recommendation.Id,
                Content = recommendation.Content,
                CodeLines = recommendation.CodeLines,
                FileName = recommendation.FileName,
                Type = recommendation.Type,
                PullRequestNumber = recommendation.PullRequestNumber,
                RepositoryId = recommendation.RepositoryId,
                Status = recommendation.Status,
                CreatedAt = recommendation.CreatedAt,
            };

        public static RecommendationEntity ToDb(this Recommendation recommendation) =>
            new RecommendationEntity
            {
                Id = recommendation.Id,
                Content = recommendation.Content,
                CodeLines = recommendation.CodeLines,
                FileName = recommendation.FileName,
                Type = recommendation.Type,
                PullRequestNumber = recommendation.PullRequestNumber,
                RepositoryId = recommendation.RepositoryId,
                Status = recommendation.Status,
                CreatedAt = recommendation.CreatedAt,
            };

        public static Comment ToDomain(this CommentEntity recommendation) =>
            new Comment
            {
                Id = recommendation.Id,
                Text = recommendation.Text,
                RecommendationId = recommendation.Id,
                CreatedAt = recommendation.CreatedAt,
                IsFromUser = recommendation.IsFromUser,
            };

        public static CommentEntity ToDb(this Comment recommendation) =>
            new CommentEntity
            {
                Id = recommendation.Id,
                Text = recommendation.Text,
                RecommendationId = recommendation.Id,
                CreatedAt = recommendation.CreatedAt,
                IsFromUser = recommendation.IsFromUser,
            };

        public static CreateReviewDtoItem ToDto(this ReviewResultResponseItem item) =>
            new CreateReviewDtoItem()
            {
                Description = item.Description,
                File = item.File,
                BeginsAtCodeLine = item.BeginsAtCodeLine,
                EndsAtCodeLine = item.EndsAtCodeLine,
            };
    }
}
