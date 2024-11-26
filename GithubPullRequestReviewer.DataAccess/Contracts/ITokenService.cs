namespace GithubPullRequestReviewer.DataAccess.Contracts
{
    public interface ITokenService
    {
        Task<bool> ValidateAndSetTokenAsync(string token);
        string GetToken();
    }
}