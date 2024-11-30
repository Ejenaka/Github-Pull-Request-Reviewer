namespace GithubPullRequestReviewer.DataAccess.Requests;

public record GetFileContentRequest(string RepositoryName, string FilePath, string HeadRef);