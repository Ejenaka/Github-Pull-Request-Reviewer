/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { Repository } from '../../models/repository';

export interface ApiUsersCurrentRepositoriesGet$Plain$Params {

/**
 * GitHub User Access Token
 */
  access_token: string;
}

export function apiUsersCurrentRepositoriesGet$Plain(http: HttpClient, rootUrl: string, params: ApiUsersCurrentRepositoriesGet$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<Repository>>> {
  const rb = new RequestBuilder(rootUrl, apiUsersCurrentRepositoriesGet$Plain.PATH, 'get');
  if (params) {
    rb.header('access_token', params.access_token, {});
  }

  return http.request(
    rb.build({ responseType: 'text', accept: 'text/plain', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<Array<Repository>>;
    })
  );
}

apiUsersCurrentRepositoriesGet$Plain.PATH = '/api/users/current/repositories';