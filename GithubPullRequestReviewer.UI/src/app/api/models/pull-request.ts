/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { PullRequestStatus } from '../models/pull-request-status';
import { User } from '../models/user';
export interface PullRequest {
  creator?: User;
  diffUrl?: string | null;
  id?: number;
  lastModifiedDate?: string;
  name?: string | null;
  number?: number;
  status?: PullRequestStatus;
}
