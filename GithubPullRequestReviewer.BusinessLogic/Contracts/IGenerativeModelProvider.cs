namespace GithubPullRequestReviewer.PullRequestAPI.Contracts
{
    public interface IGenerativeModelProvider
    {
        Task<string> SendPromptTextAsync(string text);
    }
}
