using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.BusinessLogic.Services;
using GithubPullRequestReviewer.DataAccess.ApiClients;
using GithubPullRequestReviewer.DataAccess.Contracts;
using GithubPullRequestReviewer.DataAccess.Options;
using GithubPullRequestReviewer.DataAccess.Services;
using GithubPullRequestReviewer.EventHandler.Authorization;
using GithubPullRequestReviewer.EventHandler.EventProcessors;
using Microsoft.AspNetCore.Authentication;
using Octokit;
using Octokit.Webhooks;
using Octokit.Webhooks.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<GithubOAuthAppOptions>(builder.Configuration.GetSection("GithubOAuthApp"));
builder.Services.Configure<ApiBaseUrlsOptions>(builder.Configuration.GetSection("ApiBaseUrls"));

builder.Services.AddControllers();
builder.Services.AddSingleton<WebhookEventProcessor, PullRequestWebhookEventProcessor>();
builder.Services.AddTransient(_ => new GitHubClient(new ProductHeaderValue("pull-request-reviewer"))
{
    Credentials = new Credentials(builder.Configuration["GitHubAccessToken"])
});
builder.Services.AddTransient<ITokenService, GithubTokenService>();
builder.Services.AddTransient<IGithubWebhookService, GithubWebhookService>();
builder.Services.AddTransient<IReviewerApiClient, ReviewerApiClient>(c =>
    new ReviewerApiClient(builder.Configuration.GetSection("ApiBaseUrls")["ReviewerApi"], c.GetRequiredService<ITokenService>()));

builder.Services
    .AddAuthentication()
    .AddScheme<AuthenticationSchemeOptions, GithubUserAuthenticationHandler>("GithubUserAuthenticationScheme", options => { });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<AccessTokenHeaderParametr>();
});

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGitHubWebhooks();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.Run();
