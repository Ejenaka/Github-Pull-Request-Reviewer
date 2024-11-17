using OpenAI.Chat;

namespace GithubPullRequestReviewer.BusinessLogic.Contracts
{
    public interface IGenerativeModelProvider
    {
        Task<string> SendMessageAsync(string text);
        Task<string> SendMessagesAsync(IEnumerable<ChatMessage> messages);
    }
}
