using GithubPullRequestReviewer.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace GithubPullRequestReviewer.ReviewerAPI.Authorization
{
    public class GithubUserAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ITokenValidator _tokenValidator;

        public GithubUserAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ITokenValidator tokenValidator)
            : base(options, logger, encoder)
        {
            _tokenValidator = tokenValidator;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var token = Request.Headers["access_token"].FirstOrDefault();

            if (await _tokenValidator.ValidateAndSetTokenAsync(token))
            {
                var claims = new[] { new Claim(ClaimTypes.Name, "CustomUser") };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            else
            {
                return AuthenticateResult.Fail("Invalid token.");
            }
        }
    }
}
