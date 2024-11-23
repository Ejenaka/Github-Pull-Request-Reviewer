import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatSlideToggleChange, MatSlideToggleModule } from '@angular/material/slide-toggle';
import { AuthService } from '../../services/auth.service';
import { Repository } from '../../api/pull-request/models';
import { UserService } from '../../api/pull-request/services';
import { GithubWebhookService } from '../../api/event-handler/services';
import { RepositoryHook } from '../../api/event-handler/models';
import { forkJoin, map, of, switchMap } from 'rxjs';
import { CommonModule } from '@angular/common';
import { RepositoryState } from '../../models/repository-state';
import { StateService } from '../../services/state.service';


@Component({
  selector: 'app-repositories',
  standalone: true,
  imports: [
    MatCardModule,
    MatButtonToggleModule,
    MatSlideToggleModule,
    CommonModule,
  ],
  templateUrl: './repositories.component.html',
  styleUrl: './repositories.component.scss'
})
export class RepositoriesComponent implements OnInit {
  repositories: Repository[];
  repositoriesWithHook: RepositoryState[];

  constructor(
    private readonly userApiService: UserService,
    private readonly webhookApiService: GithubWebhookService,
    private readonly authService: AuthService,
    private readonly stateService: StateService,
    private readonly cdRef: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.userApiService.apiUsersCurrentRepositoriesGet$Json({ access_token: this.authService.getAccessToken() })
      .pipe(
        switchMap(repos => {
          const hooksRequests = repos.map(repo =>
            this.webhookApiService.apiRepositoriesRepositoryIdGithubWebhooksGet$Json({ repositoryId: repo.id as number, access_token: this.authService.getAccessToken() })
              .pipe(map(hooks => ({
                repository: repo,
                webhook: hooks.find(x => x?.config!['url'].includes('api/github/webhooks')),
                configured: !!hooks.find(x => x?.config!['url'].includes('api/github/webhooks'))
              }) as RepositoryState))
          );
          return forkJoin(hooksRequests);
        }))
      .subscribe(repos => {
        this.repositoriesWithHook = repos.sort((a, b) => +!!b.webhook - +!!a.webhook);
        this.stateService.repositories = this.repositoriesWithHook;
      });
  }

  onConfigureToggleChanged(data: MatSlideToggleChange) {
    const repositoryId = +data.source.id;
    const repositoryWithHook = this.repositoriesWithHook.find(x => x.repository.id === repositoryId);
    if (!repositoryWithHook) {
      throw new Error(`Repository ${repositoryId} not found`);
    }

    if (data.checked) {
      this.webhookApiService.apiGithubWebhooksPost({ repositoryId: repositoryId, access_token: this.authService.getAccessToken() })
        .subscribe(() => {
          repositoryWithHook!.configured = true;
          this.cdRef.detectChanges();
        });
    }
  }
}
