using GithubPullRequestReviewer.PullRequestAPI.Services;
using GithubPullRequestReviewer.DataAccess;
using Microsoft.EntityFrameworkCore;
using Octokit;
using GithubPullRequestReviewer.PullRequestAPI.Authorization;
using GithubPullRequestReviewer.BusinessLogic.Contracts;
using GithubPullRequestReviewer.BusinessLogic.Services;
using Microsoft.AspNetCore.Authentication;
using GithubPullRequestReviewer.BusinessLogic;
using GithubPullRequestReviewer.DataAccess.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<GithubOAuthAppOptions>(builder.Configuration.GetSection("GithubOAuthApp"));
builder.Services.Configure<ApiBaseUrlsOptions>(builder.Configuration.GetSection("ApiBaseUrls"));

builder.Services.AddDbContext<PullRequestReviewerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddScoped(_ => new GitHubClient(new ProductHeaderValue("pull-request-reviewer")));
builder.Services.AddScoped<ITokenService, GithubTokenService>();
builder.Services.AddScoped<ITokenValidator, GithubTokenValidator>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRepositoryService, RepositoryService>();
builder.Services.AddTransient<IPullRequestService, PullRequestService>();
builder.Services.AddTransient<IReviewService, ReviewService>();
builder.Services.AddTransient<ICommentService, CommentService>();

builder.Services
    .AddAuthentication(" GithubUserAuthenticationScheme")
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

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.Run();
