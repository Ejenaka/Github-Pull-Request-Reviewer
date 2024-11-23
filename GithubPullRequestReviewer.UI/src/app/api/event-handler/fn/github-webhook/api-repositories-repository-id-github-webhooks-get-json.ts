/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { RepositoryHook } from '../../models/repository-hook';

export interface ApiRepositoriesRepositoryIdGithubWebhooksGet$Json$Params {
  repositoryId: number;

/**
 * GitHub User Access Token
 */
  access_token: string;
}

export function apiRepositoriesRepositoryIdGithubWebhooksGet$Json(http: HttpClient, rootUrl: string, params: ApiRepositoriesRepositoryIdGithubWebhooksGet$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<RepositoryHook>>> {
  const rb = new RequestBuilder(rootUrl, apiRepositoriesRepositoryIdGithubWebhooksGet$Json.PATH, 'get');
  if (params) {
    rb.path('repositoryId', params.repositoryId, {});
    rb.header('access_token', params.access_token, {});
  }

  return http.request(
    rb.build({ responseType: 'json', accept: 'text/json', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<Array<RepositoryHook>>;
    })
  );
}

apiRepositoriesRepositoryIdGithubWebhooksGet$Json.PATH = '/api/repositories/{repositoryId}/github-webhooks';
