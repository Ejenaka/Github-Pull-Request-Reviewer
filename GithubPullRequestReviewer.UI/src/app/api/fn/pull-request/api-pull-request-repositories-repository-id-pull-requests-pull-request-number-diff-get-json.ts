/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';


export interface ApiPullRequestRepositoriesRepositoryIdPullRequestsPullRequestNumberDiffGet$Json$Params {
  repositoryId: number;
  pullRequestNumber: number;

/**
 * GitHub User Access Token
 */
  access_token: string;
}

export function apiPullRequestRepositoriesRepositoryIdPullRequestsPullRequestNumberDiffGet$Json(http: HttpClient, rootUrl: string, params: ApiPullRequestRepositoriesRepositoryIdPullRequestsPullRequestNumberDiffGet$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<string>> {
  const rb = new RequestBuilder(rootUrl, apiPullRequestRepositoriesRepositoryIdPullRequestsPullRequestNumberDiffGet$Json.PATH, 'get');
  if (params) {
    rb.path('repositoryId', params.repositoryId, {});
    rb.path('pullRequestNumber', params.pullRequestNumber, {});
    rb.header('access_token', params.access_token, {});
  }

  return http.request(
    rb.build({ responseType: 'json', accept: 'text/json', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<string>;
    })
  );
}

apiPullRequestRepositoriesRepositoryIdPullRequestsPullRequestNumberDiffGet$Json.PATH = '/api/PullRequest/repositories/{repositoryId}/pull-requests/{pullRequestNumber}/diff';