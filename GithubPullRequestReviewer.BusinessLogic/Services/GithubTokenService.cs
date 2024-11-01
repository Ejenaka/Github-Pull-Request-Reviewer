using GithubPullRequestReviewer.BusinessLogic.Contracts;

namespace GithubPullRequestReviewer.BusinessLogic.Services
{
    public class GithubTokenService : ITokenService
    {
        private string? _token;

        public string GetToken()
        {
            return _token;
        }

        public void SaveToken(string token)
        {
            _token = token;
        }
    }
}
