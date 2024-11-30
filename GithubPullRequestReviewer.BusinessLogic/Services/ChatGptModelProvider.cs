using GithubPullRequestReviewer.BusinessLogic.Contracts;
using OpenAI.Chat;

namespace GithubPullRequestReviewer.BusinessLogic.Services;

public class ChatGptModelProvider : IGenerativeModelProvider
{
    private readonly ChatClient _chatClient;

    public ChatGptModelProvider(string apiKey)
    {
        _chatClient = new ChatClient("gpt-4", apiKey);
    }

    public async Task<string> SendMessageAsync(string text)
    {
        var chatResult = await _chatClient.CompleteChatAsync(text);
        return chatResult.Value.Content[0].Text;
    }

    public async Task<string> SendMessagesAsync(IEnumerable<ChatMessage> messages)
    {
        var chatResult = await _chatClient.CompleteChatAsync(messages);
        return chatResult.Value.Content.Last().Text;
    }
}