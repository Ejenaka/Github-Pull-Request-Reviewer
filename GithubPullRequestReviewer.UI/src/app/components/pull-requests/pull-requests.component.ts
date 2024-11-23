import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { GithubWebhookService } from '../../api/event-handler/services';
import { PullRequestService, UserService } from '../../api/pull-request/services';
import { AuthService } from '../../services/auth.service';
import { RepositoryState } from '../../models/repository-state';
import { StateService } from '../../services/state.service';
import { PullRequest } from '../../api/pull-request/models';
import { from, of, scan, switchMap } from 'rxjs';
import { PullRequestState } from '../../models/pull-request-state';

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
  repositories: RepositoryState[];
  pullRequests: PullRequest[] = [];
  pullRequestsState: PullRequestState[] = [];

  constructor(
    private readonly userApiService: UserService,
    private readonly pullRequestApiService: PullRequestService,
    private readonly authService: AuthService,
    private readonly stateService: StateService,
    private readonly cdRef: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.repositories = this.stateService.repositories;

    from(this.repositories).pipe(
      switchMap(repo => this.pullRequestApiService.apiPullRequestRepositoriesRepositoryIdPullRequestsGet$Json({ repositoryId: repo.repository.id as number, access_token: this.authService.getAccessToken() }))
    ).subscribe(pullRequests => this.pullRequests.push(...pullRequests));
  }
}
