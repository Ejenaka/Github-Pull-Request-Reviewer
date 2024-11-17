using GithubPullRequestReviewer.PullRequestAPI.Services;
using GithubPullRequestReviewer.DataAccess;
using Microsoft.EntityFrameworkCore;
using Octokit;
using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.BusinessLogic.Services;
using Microsoft.AspNetCore.Authentication;
using GithubPullRequestReviewer.BusinessLogic;
using GithubPullRequestReviewer.DataAccess.ApiClients;
using GithubPullRequestReviewer.DataAccess.Contracts;
using GithubPullRequestReviewer.DataAccess.Options;
using GithubPullRequestReviewer.ReviewerAPI.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<GithubOAuthAppOptions>(builder.Configuration.GetSection("GithubOAuthApp"));

builder.Services.AddDbContext<PullRequestReviewerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddScoped(_ => new GitHubClient(new ProductHeaderValue("pull-request-reviewer")));
builder.Services.AddScoped<ITokenService, GithubTokenService>();
builder.Services.AddScoped<ITokenValidator, GithubTokenValidator>();
builder.Services.AddTransient<IPullRequestService, PullRequestService>();
builder.Services.AddTransient<IPullRequestReviewer, PullRequestReviewer>();
// Mock for testing purposes
builder.Services.AddTransient<IPullRequestReviewer, PullRequestReviewerMock>();
// builder.Services.AddTransient<IGenerativeModelProvider, ChatGptModelProvider>(_
//     => new ChatGptModelProvider(builder.Configuration["OpenAI:ApiKey"]));

builder.Services.AddTransient<IPullRequestApiClient, PullRequestApiClient>(c =>
    new PullRequestApiClient(builder.Configuration.GetSection("ApiBaseUrls")["PullRequestApi"]));
builder.Services.AddTransient<IReviewApiClient, ReviewApiClient>(c =>
    new ReviewApiClient(builder.Configuration.GetSection("ApiBaseUrls")["PullRequestApi"]));
builder.Services.AddTransient<ICommentApiClient, CommentApiClient>(c =>
    new CommentApiClient(builder.Configuration.GetSection("ApiBaseUrls")["PullRequestApi"]));

builder.Services
    .AddAuthentication("GithubUserAuthenticationScheme")
    .AddScheme<AuthenticationSchemeOptions, GithubUserAuthenticationHandler>("GithubUserAuthenticationScheme", options => { });

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<AccessTokenHeaderParametr>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
