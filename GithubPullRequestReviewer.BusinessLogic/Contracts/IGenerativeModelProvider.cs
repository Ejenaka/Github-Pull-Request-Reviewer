namespace GithubPullRequestReviewer.PullRequestAPI.Contracts
{
    public interface IGenerativeModelProvider
    {
        Task<string> SendTextAsync(string text);
    }
}
