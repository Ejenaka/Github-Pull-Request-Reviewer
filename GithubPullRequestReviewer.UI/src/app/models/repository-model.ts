import { RepositoryHook } from '../api/event-handler/models';
import { Repository } from '../api/pull-request/models';

export interface RepositoryModel {
    repository: Repository,
    webhook?: RepositoryHook,
    configured: boolean,
}
