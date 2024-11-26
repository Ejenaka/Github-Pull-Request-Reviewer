import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { GithubWebhookService } from '../../api/event-handler/services';
import { PullRequestService, UserService } from '../../api/pull-request/services';
import { AuthService } from '../../services/auth.service';
import { RepositoryModel } from '../../models/repository-model';
import { StateService } from '../../services/state.service';
import { PullRequest } from '../../api/pull-request/models';
import { concatAll, filter, forkJoin, from, mergeMap, of, scan, switchMap } from 'rxjs';
import { PullRequestState } from '../../models/pull-request-state';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-pull-requests',
  standalone: true,
  imports: [
    MatCardModule,
    MatButtonToggleModule,
    MatSlideToggleModule,
    CommonModule,
  ],
  templateUrl: './pull-requests.component.html',
  styleUrl: './pull-requests.component.scss'
})
export class PullRequestsComponent implements OnInit {
  pullRequests: PullRequest[] = [];

  constructor(
    private readonly apiService: ApiService,
    private readonly userApiService: UserService,
    private readonly pullRequestApiService: PullRequestService,
    private readonly authService: AuthService,
    private readonly stateService: StateService,
    private readonly cdRef: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.apiService.getUserRepositories()
      .pipe(
        mergeMap(repos => {
          const reposId = repos.filter(r => r.configured).map(r => r.repository.id) as number[];
          return this.apiService.getPullRequestsByRepositories(reposId);
        })
      )
      .subscribe(pullRequests => this.pullRequests.push(...pullRequests));
  }
}
