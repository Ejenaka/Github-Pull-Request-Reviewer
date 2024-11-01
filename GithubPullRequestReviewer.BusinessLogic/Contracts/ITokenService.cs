namespace GithubPullRequestReviewer.BusinessLogic.Contracts
{
    public interface ITokenService
    {
        string GetToken();
        void SaveToken(string token);
    }
}