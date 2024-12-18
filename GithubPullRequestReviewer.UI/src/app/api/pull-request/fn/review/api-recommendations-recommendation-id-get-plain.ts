/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { Recommendation } from '../../models/recommendation';

export interface ApiRecommendationsRecommendationIdGet$Plain$Params {
  recommendationId: number;

/**
 * GitHub User Access Token
 */
  access_token: string;
}

export function apiRecommendationsRecommendationIdGet$Plain(http: HttpClient, rootUrl: string, params: ApiRecommendationsRecommendationIdGet$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<Recommendation>> {
  const rb = new RequestBuilder(rootUrl, apiRecommendationsRecommendationIdGet$Plain.PATH, 'get');
  if (params) {
    rb.path('recommendationId', params.recommendationId, {});
    rb.header('access_token', params.access_token, {});
  }

  return http.request(
    rb.build({ responseType: 'text', accept: 'text/plain', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<Recommendation>;
    })
  );
}

apiRecommendationsRecommendationIdGet$Plain.PATH = '/api/recommendations/{recommendationId}';
