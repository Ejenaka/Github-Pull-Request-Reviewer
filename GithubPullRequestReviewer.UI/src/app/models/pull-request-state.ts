import { PullRequest, Repository } from '../api/pull-request/models';

export interface PullRequestState {
    pullRequest: PullRequest;
    repository: Repository,
}
