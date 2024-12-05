using GithubPullRequestReviewer.DataAccess;
using Microsoft.EntityFrameworkCore;
using Octokit;
using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.BusinessLogic.Services;
using Microsoft.AspNetCore.Authentication;
using GithubPullRequestReviewer.DataAccess.ApiClients;
using GithubPullRequestReviewer.DataAccess.Contracts;
using GithubPullRequestReviewer.DataAccess.Options;
using GithubPullRequestReviewer.DataAccess.Services;
using GithubPullRequestReviewer.ReviewerAPI.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<GithubOAuthAppOptions>(builder.Configuration.GetSection("GithubOAuthApp"));

builder.Services.AddDbContext<PullRequestReviewerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddScoped(c => new GitHubClient(new ProductHeaderValue("pull-request-reviewer")));
builder.Services.AddScoped<ITokenService, GithubTokenService>();
builder.Services.AddTransient<IGenerativeModelProvider, ChatGptModelProvider>(_ => new ChatGptModelProvider(builder.Configuration["OpenAI:ApiKey"]));
builder.Services.AddTransient<IPullRequestReviewer, PullRequestReviewer>();

builder.Services.AddTransient<IPullRequestApiClient, PullRequestApiClient>(c =>
    new PullRequestApiClient(builder.Configuration.GetSection("ApiBaseUrls")["PullRequestApi"], c.GetRequiredService<ITokenService>()));
builder.Services.AddTransient<IReviewApiClient, ReviewApiClient>(c =>
    new ReviewApiClient(builder.Configuration.GetSection("ApiBaseUrls")["PullRequestApi"], c.GetRequiredService<ITokenService>()));
builder.Services.AddTransient<ICommentApiClient, CommentApiClient>(c =>
    new CommentApiClient(builder.Configuration.GetSection("ApiBaseUrls")["PullRequestApi"], c.GetRequiredService<ITokenService>()));

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

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.Run();
