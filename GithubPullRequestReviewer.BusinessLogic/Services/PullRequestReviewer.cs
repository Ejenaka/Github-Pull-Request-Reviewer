using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.DataAccess.Responses;
using System.Text.Json;
using GithubPullRequestReviewer.DataAccess.Contracts;
using GithubPullRequestReviewer.DataAccess.Requests;
using GithubPullRequestReviewer.Domain.DTO;
using GithubPullRequestReviewer.Domain.Models;
using OpenAI.Chat;

namespace GithubPullRequestReviewer.BusinessLogic.Services;

public class PullRequestReviewer : IPullRequestReviewer
{
    private readonly IGenerativeModelProvider _generativeModelProvider;
    private readonly IPullRequestApiClient _pullRequestApiClient;
    private readonly IReviewApiClient _reviewApiClient;
    private readonly ICommentApiClient _commentApiClient;

    public PullRequestReviewer(
        IGenerativeModelProvider generativeModelProvider,
        IPullRequestApiClient pullRequestApiClient,
        IReviewApiClient reviewApiClient,
        ICommentApiClient commentApiClient)
    {
        _generativeModelProvider = generativeModelProvider;
        _pullRequestApiClient = pullRequestApiClient;
        _reviewApiClient = reviewApiClient;
        _commentApiClient = commentApiClient;
    }

    public async Task<ReviewResultResponse> ReviewPullRequestAsync(long repositoryId, int pullRequestNumber)
    {
        var pullRequestDetails = await _pullRequestApiClient.GetPullRequestAsync(repositoryId, pullRequestNumber);
        var pullRequestDiffContent = await _pullRequestApiClient.GetPullRequestDiffContentAsync(repositoryId, pullRequestNumber);
        var previousPullRequestReviewResult = await _reviewApiClient.GetPullRequestReviewResultAsync(repositoryId, pullRequestNumber);

        var  reviewRequestPrompt = previousPullRequestReviewResult != null ?
            Prompts.BuildPromptForReview(pullRequestDetails.Name, pullRequestDiffContent, previousPullRequestReviewResult) : 
            Prompts.BuildPromptForReview(pullRequestDetails.Name, pullRequestDiffContent);

        var modelResponse = await _generativeModelProvider.SendMessageAsync(reviewRequestPrompt);
            
        if (string.IsNullOrEmpty(modelResponse))
        {
            return null;
        }

        var reviewResult = JsonSerializer.Deserialize<ReviewResultResponse>(modelResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var createReviewRequest = new CreateReviewDto()
        {
            PullRequestNumber = pullRequestNumber,
            RepositoryId = repositoryId,
            Issues = reviewResult.Issues.Select(i => i.ToDto()).ToList(),
            Vulnerabilities = reviewResult.Vulnerabilities.Select(i => i.ToDto()).ToList(),
            Optimizations = reviewResult.Optimization.Select(i => i.ToDto()).ToList(),
            Enhancements = reviewResult.Enhancements.Select(i => i.ToDto()).ToList(),
            BestPractices = reviewResult.BestPractices.Select(i => i.ToDto()).ToList(),
        };
            
        await _reviewApiClient.CreatePullRequestReviewAsync(createReviewRequest);

        return reviewResult;
    }

    public async Task<Comment> AddCommentForRecommendationAndGetResponseAsync(CreateCommentRequest createCommentRequest)
    {
        var recommendationId = createCommentRequest.RecommendationId;
        var recommendation = await _reviewApiClient.GetRecommendationByIdAsync(recommendationId);
        var recommendationComments = await _commentApiClient.GetCommentsForRecommendationAsync(recommendationId);

        var messagesForGenerativeModel = new List<ChatMessage>()
        {
            new UserChatMessage(Prompts.CommentPrompt),
            new SystemChatMessage(Prompts.CommentPromptModelResponse),
        };
            
        messagesForGenerativeModel.AddRange(recommendationComments
            .OrderBy(c => c.CreatedAt)
            .Select<Comment, ChatMessage>(c => c.IsFromUser ? new UserChatMessage(c.Text) : new SystemChatMessage(c.Text))
        );

        var userCommentRequest = Prompts.BuildCommentRequest(
            createCommentRequest.Text,
            recommendation.FileName,
            recommendation.Content);
            
        messagesForGenerativeModel.Add(new UserChatMessage(userCommentRequest));
            
        var modelResponse = await _generativeModelProvider.SendMessagesAsync(messagesForGenerativeModel);
        var modelCreateCommentRequest = new CreateCommentRequest(modelResponse, false, recommendationId);
            
        await _commentApiClient.CreateCommentForRecommendationAsync(createCommentRequest);
        await _commentApiClient.CreateCommentForRecommendationAsync(modelCreateCommentRequest);
            
        return new Comment
        {
            RecommendationId = modelCreateCommentRequest.RecommendationId,
            Text = modelCreateCommentRequest.Text,
            IsFromUser = modelCreateCommentRequest.IsFromUser,
            CreatedAt = DateTime.Now,
        };
    }
}