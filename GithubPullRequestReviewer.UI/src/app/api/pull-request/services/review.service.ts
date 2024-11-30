/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';

import { apiRecommendationsPatch } from '../fn/review/api-recommendations-patch';
import { ApiRecommendationsPatch$Params } from '../fn/review/api-recommendations-patch';
import { apiRecommendationsRecommendationIdGet$Json } from '../fn/review/api-recommendations-recommendation-id-get-json';
import { ApiRecommendationsRecommendationIdGet$Json$Params } from '../fn/review/api-recommendations-recommendation-id-get-json';
import { apiRecommendationsRecommendationIdGet$Plain } from '../fn/review/api-recommendations-recommendation-id-get-plain';
import { ApiRecommendationsRecommendationIdGet$Plain$Params } from '../fn/review/api-recommendations-recommendation-id-get-plain';
import { apiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Json } from '../fn/review/api-repositories-repository-id-pull-requests-pull-request-number-recommendations-get-json';
import { ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Json$Params } from '../fn/review/api-repositories-repository-id-pull-requests-pull-request-number-recommendations-get-json';
import { apiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Plain } from '../fn/review/api-repositories-repository-id-pull-requests-pull-request-number-recommendations-get-plain';
import { ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Plain$Params } from '../fn/review/api-repositories-repository-id-pull-requests-pull-request-number-recommendations-get-plain';
import { apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Json } from '../fn/review/api-repositories-repository-id-pull-requests-pull-request-number-review-get-json';
import { ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Json$Params } from '../fn/review/api-repositories-repository-id-pull-requests-pull-request-number-review-get-json';
import { apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Plain } from '../fn/review/api-repositories-repository-id-pull-requests-pull-request-number-review-get-plain';
import { ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Plain$Params } from '../fn/review/api-repositories-repository-id-pull-requests-pull-request-number-review-get-plain';
import { apiReviewsPost } from '../fn/review/api-reviews-post';
import { ApiReviewsPost$Params } from '../fn/review/api-reviews-post';
import { Recommendation } from '../models/recommendation';
import { ReviewResult } from '../models/review-result';

@Injectable({ providedIn: 'root' })
export class ReviewService extends BaseService {
  constructor(config: ApiConfiguration, http: HttpClient) {
    super(config, http);
  }

  /** Path part for operation `apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet()` */
  static readonly ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGetPath = '/api/repositories/{repositoryId}/pull-requests/{pullRequestNumber}/review';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Plain$Response(params: ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<ReviewResult>> {
    return apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Plain(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Plain(params: ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Plain$Params, context?: HttpContext): Observable<ReviewResult> {
    return this.apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<ReviewResult>): ReviewResult => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Json()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Json$Response(params: ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<ReviewResult>> {
    return apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Json(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Json$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Json(params: ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Json$Params, context?: HttpContext): Observable<ReviewResult> {
    return this.apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Json$Response(params, context).pipe(
      map((r: StrictHttpResponse<ReviewResult>): ReviewResult => r.body)
    );
  }

  /** Path part for operation `apiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet()` */
  static readonly ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGetPath = '/api/repositories/{repositoryId}/pull-requests/{pullRequestNumber}/recommendations';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Plain$Response(params: ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<Recommendation>>> {
    return apiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Plain(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Plain(params: ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Plain$Params, context?: HttpContext): Observable<Array<Recommendation>> {
    return this.apiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<Recommendation>>): Array<Recommendation> => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Json()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Json$Response(params: ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<Recommendation>>> {
    return apiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Json(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Json$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Json(params: ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Json$Params, context?: HttpContext): Observable<Array<Recommendation>> {
    return this.apiRepositoriesRepositoryIdPullRequestsPullRequestNumberRecommendationsGet$Json$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<Recommendation>>): Array<Recommendation> => r.body)
    );
  }

  /** Path part for operation `apiRecommendationsRecommendationIdGet()` */
  static readonly ApiRecommendationsRecommendationIdGetPath = '/api/recommendations/{recommendationId}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiRecommendationsRecommendationIdGet$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRecommendationsRecommendationIdGet$Plain$Response(params: ApiRecommendationsRecommendationIdGet$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<Recommendation>> {
    return apiRecommendationsRecommendationIdGet$Plain(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiRecommendationsRecommendationIdGet$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRecommendationsRecommendationIdGet$Plain(params: ApiRecommendationsRecommendationIdGet$Plain$Params, context?: HttpContext): Observable<Recommendation> {
    return this.apiRecommendationsRecommendationIdGet$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<Recommendation>): Recommendation => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiRecommendationsRecommendationIdGet$Json()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRecommendationsRecommendationIdGet$Json$Response(params: ApiRecommendationsRecommendationIdGet$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<Recommendation>> {
    return apiRecommendationsRecommendationIdGet$Json(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiRecommendationsRecommendationIdGet$Json$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRecommendationsRecommendationIdGet$Json(params: ApiRecommendationsRecommendationIdGet$Json$Params, context?: HttpContext): Observable<Recommendation> {
    return this.apiRecommendationsRecommendationIdGet$Json$Response(params, context).pipe(
      map((r: StrictHttpResponse<Recommendation>): Recommendation => r.body)
    );
  }

  /** Path part for operation `apiRecommendationsPatch()` */
  static readonly ApiRecommendationsPatchPath = '/api/recommendations';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiRecommendationsPatch()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiRecommendationsPatch$Response(params: ApiRecommendationsPatch$Params, context?: HttpContext): Observable<StrictHttpResponse<void>> {
    return apiRecommendationsPatch(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiRecommendationsPatch$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiRecommendationsPatch(params: ApiRecommendationsPatch$Params, context?: HttpContext): Observable<void> {
    return this.apiRecommendationsPatch$Response(params, context).pipe(
      map((r: StrictHttpResponse<void>): void => r.body)
    );
  }

  /** Path part for operation `apiReviewsPost()` */
  static readonly ApiReviewsPostPath = '/api/reviews';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiReviewsPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiReviewsPost$Response(params: ApiReviewsPost$Params, context?: HttpContext): Observable<StrictHttpResponse<void>> {
    return apiReviewsPost(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiReviewsPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiReviewsPost(params: ApiReviewsPost$Params, context?: HttpContext): Observable<void> {
    return this.apiReviewsPost$Response(params, context).pipe(
      map((r: StrictHttpResponse<void>): void => r.body)
    );
  }

}
