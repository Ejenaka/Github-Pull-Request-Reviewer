/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { PullRequest } from '../../models/pull-request';

export interface ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberGet$Plain$Params {
  repositoryId: number;
  pullRequestNumber: number;

/**
 * GitHub User Access Token
 */
  access_token: string;
}

export function apiRepositoriesRepositoryIdPullRequestsPullRequestNumberGet$Plain(http: HttpClient, rootUrl: string, params: ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberGet$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<PullRequest>> {
  const rb = new RequestBuilder(rootUrl, apiRepositoriesRepositoryIdPullRequestsPullRequestNumberGet$Plain.PATH, 'get');
  if (params) {
    rb.path('repositoryId', params.repositoryId, {});
    rb.path('pullRequestNumber', params.pullRequestNumber, {});
    rb.header('access_token', params.access_token, {});
  }

  return http.request(
    rb.build({ responseType: 'text', accept: 'text/plain', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<PullRequest>;
    })
  );
}

apiRepositoriesRepositoryIdPullRequestsPullRequestNumberGet$Plain.PATH = '/api/repositories/{repositoryId}/pull-requests/{pullRequestNumber}';