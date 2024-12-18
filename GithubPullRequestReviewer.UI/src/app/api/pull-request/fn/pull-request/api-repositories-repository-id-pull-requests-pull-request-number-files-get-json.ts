/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { PullRequestFile } from '../../models/pull-request-file';

export interface ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberFilesGet$Json$Params {
  repositoryId: number;
  pullRequestNumber: number;

/**
 * GitHub User Access Token
 */
  access_token: string;
}

export function apiRepositoriesRepositoryIdPullRequestsPullRequestNumberFilesGet$Json(http: HttpClient, rootUrl: string, params: ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberFilesGet$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<PullRequestFile>>> {
  const rb = new RequestBuilder(rootUrl, apiRepositoriesRepositoryIdPullRequestsPullRequestNumberFilesGet$Json.PATH, 'get');
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
      return r as StrictHttpResponse<Array<PullRequestFile>>;
    })
  );
}

apiRepositoriesRepositoryIdPullRequestsPullRequestNumberFilesGet$Json.PATH = '/api/repositories/{repositoryId}/pull-requests/{pullRequestNumber}/files';
