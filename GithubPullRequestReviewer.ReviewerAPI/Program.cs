using GithubPullRequestReviewer.PullRequestAPI.Contracts;
using GithubPullRequestReviewer.PullRequestAPI.Services;
using GithubPullRequestReviewer.DataAccess;
using Microsoft.EntityFrameworkCore;
using Octokit;
using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.BusinessLogic.Services;
using Microsoft.AspNetCore.Authentication;
using GithubPullRequestReviewer.BusinessLogic;
using GithubPullRequestReviewer.ReviewerAPI.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<GithubOAuthAppOptions>(builder.Configuration.GetSection("GithubOAuthApp"));

builder.Services.AddDbContext<PullRequestReviewerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddScoped(_ => new GitHubClient(new ProductHeaderValue("pull-request-reviewer")));
builder.Services.AddScoped<ITokenService, GithubTokenService>();
builder.Services.AddScoped<ITokenValidator, GithubTokenValidator>();
builder.Services.AddTransient<IGithubProvider, GithubProvider>();
builder.Services.AddTransient<IPullRequestService, PullRequestService>();
builder.Services.AddTransient<IPullRequestReviewer, PullRequestReviewer>();
// Mock for testing purposes
//builder.Services.AddTransient<IPullRequestReviewer, PullRequestReviewerMock>();
builder.Services.AddTransient<IGenerativeModelProvider, ChatGptModelProvider>(_
    => new ChatGptModelProvider(builder.Configuration["OpenAI:ApiKey"]));

builder.Services
    .AddAuthentication(" GithuhUserAuthenticationScheme")
    .AddScheme<AuthenticationSchemeOptions, GithuhUserAuthenticationHandler>("GithuhUserAuthenticationScheme", options => { });

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
