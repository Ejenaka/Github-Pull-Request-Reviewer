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

import { apiCommentsPost$Json } from '../fn/reviewer/api-comments-post-json';
import { ApiCommentsPost$Json$Params } from '../fn/reviewer/api-comments-post-json';
import { apiCommentsPost$Plain } from '../fn/reviewer/api-comments-post-plain';
import { ApiCommentsPost$Plain$Params } from '../fn/reviewer/api-comments-post-plain';
import { apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Json } from '../fn/reviewer/api-repositories-repository-id-pull-requests-pull-request-number-review-get-json';
import { ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Json$Params } from '../fn/reviewer/api-repositories-repository-id-pull-requests-pull-request-number-review-get-json';
import { apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Plain } from '../fn/reviewer/api-repositories-repository-id-pull-requests-pull-request-number-review-get-plain';
import { ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Plain$Params } from '../fn/reviewer/api-repositories-repository-id-pull-requests-pull-request-number-review-get-plain';
import { Comment } from '../models/comment';
import { ReviewResultResponse } from '../models/review-result-response';

@Injectable({ providedIn: 'root' })
export class ReviewerService extends BaseService {
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
  apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Plain$Response(params: ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<ReviewResultResponse>> {
    return apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Plain(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Plain(params: ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Plain$Params, context?: HttpContext): Observable<ReviewResultResponse> {
    return this.apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<ReviewResultResponse>): ReviewResultResponse => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Json()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Json$Response(params: ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<ReviewResultResponse>> {
    return apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Json(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Json$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Json(params: ApiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Json$Params, context?: HttpContext): Observable<ReviewResultResponse> {
    return this.apiRepositoriesRepositoryIdPullRequestsPullRequestNumberReviewGet$Json$Response(params, context).pipe(
      map((r: StrictHttpResponse<ReviewResultResponse>): ReviewResultResponse => r.body)
    );
  }

  /** Path part for operation `apiCommentsPost()` */
  static readonly ApiCommentsPostPath = '/api/comments';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiCommentsPost$Plain()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiCommentsPost$Plain$Response(params: ApiCommentsPost$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<Comment>> {
    return apiCommentsPost$Plain(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiCommentsPost$Plain$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiCommentsPost$Plain(params: ApiCommentsPost$Plain$Params, context?: HttpContext): Observable<Comment> {
    return this.apiCommentsPost$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<Comment>): Comment => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiCommentsPost$Json()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiCommentsPost$Json$Response(params: ApiCommentsPost$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<Comment>> {
    return apiCommentsPost$Json(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `apiCommentsPost$Json$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiCommentsPost$Json(params: ApiCommentsPost$Json$Params, context?: HttpContext): Observable<Comment> {
    return this.apiCommentsPost$Json$Response(params, context).pipe(
      map((r: StrictHttpResponse<Comment>): Comment => r.body)
    );
  }

}
