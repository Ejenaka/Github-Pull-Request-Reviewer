import { Injectable } from '@angular/core';
import { GithubWebhookService } from '../api/event-handler/services';
import { CommentService, PullRequestService, ReviewService, UserService } from '../api/pull-request/services';
import { AuthService } from './auth.service';
import { switchMap, map, forkJoin, Observable, from, mergeMap } from 'rxjs';
import { RepositoryModel } from '../models/repository-model';
import { GetFileContentRequest, PullRequest, PullRequestFile, Recommendation, User, Comment } from '../api/pull-request/models';
import { CreateCommentRequest } from '../api/reviewer/models';
import { ReviewerService } from '../api/reviewer/services';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(
    private readonly pullRequestsApiService: PullRequestService,
    private readonly userApiService: UserService,
    private readonly rewievApiService: ReviewService,
    private readonly webhookApiService: GithubWebhookService,
    private readonly commentsApiService: CommentService,
    private readonly reviewerApiService: ReviewerService,
    private readonly authService: AuthService,
  ) { }

  getUserRepositories(): Observable<RepositoryModel[]> {
    return this.userApiService.apiUsersCurrentRepositoriesGet$Json({ access_token: this.authService.getAccessToken() })
      .pipe(
        switchMap(repos => {
          const hooksRequests = repos.map(repo =>
            this.webhookApiService.apiRepositoriesRepositoryIdGithubWebhooksGet$Json({ repositoryId: repo.id as number, access_token: this.authService.getAccessToken() })
              .pipe(map(hooks => ({
                repository: repo,
                webhook: hooks.find(x => x?.config!['url'].includes('api/github/webhooks')),
                configured: !!hooks.find(x => x?.config!['url'].includes('api/github/webhooks'))
              }) as RepositoryModel))
          );
          return forkJoin(hooksRequests);
        }));
  }

  getPullRequestsByRepository(repoId: number): Observable<PullRequest[]> {
    return this.pullRequestsApiService.apiRepositoriesRepositoryIdPullRequestsGet$Json({
      repositoryId: repoId,
      access_token: this.authService.getAccessToken()
    });
  }

  getPullRequestsByRepositories(repositoriesId: number[]): Observable<PullRequest[]> {
    return from(repositoriesId)
      .pipe(
        mergeMap(repoId => this.pullRequestsApiService.apiRepositoriesRepositoryIdPullRequestsGet$Json({
          repositoryId: repoId,
          access_token: this.authService.getAccessToken()
        })));
  }

  getRecommendations(repoId: number, pullRequestNumber: number): Observable<Recommendation[]> {
    return this.rewievApiService.apiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Json({
      repositoryId: repoId,
      pullRequestNumber: pullRequestNumber,
      access_token: this.authService.getAccessToken()
    });
  }

  getPullRequestFiles(repoId: number, pullRequestNumber: number): Observable<PullRequestFile[]> {
    return this.pullRequestsApiService.apiRepositoriesRepositoryIdPullRequestsPullRequestNumberFilesGet$Json({
      repositoryId: repoId,
      pullRequestNumber: pullRequestNumber,
      access_token: this.authService.getAccessToken()
    });
  }

  getAuthenticatedUser(): Observable<User> {
    return this.userApiService.apiUsersCurrentGet$Json({
      access_token: this.authService.getAccessToken()
    });
  }

  getFileContent(getFileRequest: GetFileContentRequest): Observable<string> {
    return this.userApiService.apiUsersFilesPost$Json({ body: getFileRequest, access_token: this.authService.getAccessToken() });
  }

  getCommentsForRecommendation(recommendationId: number): Observable<Comment[]> {
    return this.commentsApiService.apiRecommendationsRecommendationIdCommentsGet$Json({ recommendationId: recommendationId, access_token: this.authService.getAccessToken() });
  }

  createCommentForRecommendation(createCommentRequest: CreateCommentRequest): Observable<Comment> {
    return this.reviewerApiService.apiCommentsPost$Json({
      body: createCommentRequest,
      access_token: this.authService.getAccessToken()
    });
  }

  updateRecommendationStatus(recommendationUpdateStatusRequest: Recommendation) {
    return this.rewievApiService.apiRecommendationsPatch({
      body: recommendationUpdateStatusRequest,
      access_token: this.authService.getAccessToken()
    });
  }
}
