using GithubPullRequestReviewer.Domain.Models;

namespace GithubPullRequestReviewer.BusinessLogic
{
    public static class MapperExtension
    {
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
