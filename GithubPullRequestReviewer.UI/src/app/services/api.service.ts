import { Injectable } from '@angular/core';
import { GithubWebhookService } from '../api/event-handler/services';
import { PullRequestService, ReviewService, UserService } from '../api/pull-request/services';
import { AuthService } from './auth.service';
import { switchMap, map, forkJoin, Observable, from, mergeMap, concatMap } from 'rxjs';
import { RepositoryModel } from '../models/repository-model';
import { PullRequest, PullRequestFile, Recommendation, User } from '../api/pull-request/models';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(
    private readonly pullRequestsApiService: PullRequestService,
    private readonly userApiService: UserService,
    private readonly rewievService: ReviewService,
    private readonly webhookApiService: GithubWebhookService,
    private readonly httpClient: HttpClient,
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
    return this.rewievService.apiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Json({
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

  getPullRequestFileContent(filePath: string, fileSha: string, repositoryName: string): Observable<string> {
    const accessToken = this.authService.getAccessToken();
    return this.getAuthenticatedUser().pipe(
      switchMap(user => {
        const fileUrl = `https://raw.githubusercontent.com/${user.username}/${repositoryName}/${fileSha}/${filePath}`;
        return this.httpClient.get(fileUrl, {
          headers: {
            Authorization: `Bearer ${accessToken}`
          }
        }) as Observable<string>;
      })
    )
  }

  getAuthenticatedUser(): Observable<User> {
    return this.userApiService.apiUsersCurrentGet$Json({
      access_token: this.authService.getAccessToken()
    });
  }
}