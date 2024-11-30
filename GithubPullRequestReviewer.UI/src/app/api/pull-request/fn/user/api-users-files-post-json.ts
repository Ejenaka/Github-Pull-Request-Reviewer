/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { GetFileContentRequest } from '../../models/get-file-content-request';

export interface ApiUsersFilesPost$Json$Params {

/**
 * GitHub User Access Token
 */
  access_token: string;
      body?: GetFileContentRequest
}

export function apiUsersFilesPost$Json(http: HttpClient, rootUrl: string, params: ApiUsersFilesPost$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<string>> {
  const rb = new RequestBuilder(rootUrl, apiUsersFilesPost$Json.PATH, 'post');
  if (params) {
    rb.header('access_token', params.access_token, {});
    rb.body(params.body, 'application/*+json');
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

apiUsersFilesPost$Json.PATH = '/api/users/files';