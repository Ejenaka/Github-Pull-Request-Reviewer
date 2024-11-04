using GithubPullRequestReviewer.PullRequestAPI.Contracts;
using OpenAI.Chat;

namespace GithubPullRequestReviewer.PullRequestAPI.Services
{
    public class ChatGptModelProvider : IGenerativeModelProvider
    {
        private readonly ChatClient _chatClient;

        public ChatGptModelProvider(string apiKey)
        {
            _chatClient = new("gpt-4", apiKey);
        }

        public async Task<string> SendTextAsync(string text)
        {
            var chatResult = await _chatClient.CompleteChatAsync(text);

            return chatResult.Value.Content[0].Text;
        }
    }
}
