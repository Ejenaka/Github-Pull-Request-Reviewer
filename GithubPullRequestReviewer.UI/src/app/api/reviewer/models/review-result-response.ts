/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { ReviewResultResponseItem } from '../models/review-result-response-item';
export interface ReviewResultResponse {
  bestPractices?: Array<ReviewResultResponseItem> | null;
  enhancements?: Array<ReviewResultResponseItem> | null;
  issues?: Array<ReviewResultResponseItem> | null;
  optimization?: Array<ReviewResultResponseItem> | null;
  vulnerabilities?: Array<ReviewResultResponseItem> | null;
}
