namespace GithubPullRequestReviewer.BusinessLogic.Contracts
{
    public interface IGenerativeModelProvider
    {
        Task<string> SendPromptTextAsync(string text);
    }
}
