using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.Domain.Models;
using System.Text.Json;
using GithubPullRequestReviewer.DataAccess.Responses;
using GithubPullRequestReviewer.PullRequestAPI.Requests;

namespace GithubPullRequestReviewer.BusinessLogic.Services
{
    public class PullRequestReviewerMock : IPullRequestReviewer
    {
        public Task<ReviewResultResponse> ReviewPullRequestAsync(long repositoryId, int pullRequestNumber)
        {
            var jsonResponse = "{\"issues\":[{\"description\":\"Missing validation for JWT token expiration in `TokenConfiguration`. Consider checking token expiration time to handle revoked or expired tokens appropriately.\",\"file\":\"CarService.Authentication/Configurations/TokenConfiguration.cs\",\"beginsAtCodeLine\":11,\"endsAtCodeLine\":22},{\"description\":\"Missing check for user authentication in `AuthenticateUser` endpoint. After password verification, ensure user authentication with a secure method.\",\"file\":\"CarService.Authentication/Controllers/AuthController.cs\",\"beginsAtCodeLine\":50,\"endsAtCodeLine\":68}],\"vulnerabilities\":[{\"description\":\"Hardcoded sensitive token information (e.g., `Secret`, `Issuer`) in `appsettings.json` and `appsettings.Development.json`. Move these values to secure environment variables.\",\"file\":\"CarService.Authentication/appsettings.json\",\"beginsAtCodeLine\":13,\"endsAtCodeLine\":18},{\"description\":\"Lack of input sanitization in SQL query for `GetByUsername` in `UsersRepository`. Use parameterized queries to prevent SQL injection.\",\"file\":\"CarService/CarService.Users.Api/Repositories/UsersRepository.cs\",\"beginsAtCodeLine\":68,\"endsAtCodeLine\":71}],\"optimization\":[{\"description\":\"Avoid redundant database calls in `GetUsers` method by batching queries or filtering data before retrieval to enhance performance.\",\"file\":\"CarService/CarService.Users.Api/Controllers/UsersController.cs\",\"beginsAtCodeLine\":60,\"endsAtCodeLine\":70}],\"enhancements\":[{\"description\":\"Use `IOptions<ITokenConfiguration>` for dependency injection of configuration settings for better testability and flexibility.\",\"file\":\"CarService.Authentication/Startup.cs\",\"beginsAtCodeLine\":35,\"endsAtCodeLine\":90}],\"bestPractices\":[{\"description\":\"`TokenConfiguration` class and interfaces should be consolidated in a single configuration file or namespace to improve maintainability.\",\"file\":\"CarService.Authentication/Configurations/TokenConfiguration.cs\",\"beginsAtCodeLine\":1,\"endsAtCodeLine\":22}]}";

            return Task.FromResult(
                JsonSerializer.Deserialize<ReviewResultResponse>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));
        }

        public Task<Comment> AddCommentForRecommendationAndGetResponseAsync(CreateCommentRequest createCommentRequest)
        {
            return Task.FromResult(new Comment
            {
                RecommendationId = createCommentRequest.RecommendationId,
                Text = "Test comment",
                IsFromUser = false,
                CreatedAt = DateTime.Now,
            });
        }
    }
}
