using GithubPullRequestReviewer.Domain.Enums;
using GithubPullRequestReviewer.Domain.Models;

namespace GithubPullRequestReviewer.PullRequestAPI
{
    public static class MapperExtension
    {
        public static PullRequest ToDomain(this Octokit.PullRequest pullRequest) =>
            new PullRequest
            {
                Id = pullRequest.Id,
                Number = pullRequest.Number,
                Name = pullRequest.Title,
                Status = (PullRequestStatus)pullRequest.State.Value,
                LastModifiedDate = pullRequest.UpdatedAt.DateTime,
                DiffUrl = pullRequest.DiffUrl,
                Creator = pullRequest.User.ToDomain(),
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
    }
}
