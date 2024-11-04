namespace GithubPullRequestReviewer.BusinessLogic.Contracts
{
    public interface ITokenValidator
    {
        Task<bool> ValidateAndSetTokenAsync(string token);
    }
}