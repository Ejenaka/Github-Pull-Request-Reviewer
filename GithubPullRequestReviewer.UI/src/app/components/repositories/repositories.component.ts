import { Component, OnInit } from '@angular/core';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatSlideToggleChange, MatSlideToggleModule } from '@angular/material/slide-toggle';
import { AuthService } from '../../services/auth.service';
import { Repository } from '../../api/pull-request/models';
import { GithubWebhookService } from '../../api/event-handler/services';
import { CommonModule } from '@angular/common';
import { RepositoryModel } from '../../models/repository-model';
import { StateService } from '../../services/state.service';
import { ApiService } from '../../services/api.service';


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
  repositoriesWithHook: RepositoryModel[];

  constructor(
    private readonly apiService: ApiService,
    private readonly webhookApiService: GithubWebhookService,
    private readonly authService: AuthService) { }

  ngOnInit(): void {
    this.apiService.getUserRepositories()
      .subscribe(repos => {
        this.repositoriesWithHook = repos.sort((a, b) => +!!b.webhook - +!!a.webhook);
      });
  }

  onConfigureToggleChanged(data: MatSlideToggleChange) {
    if (data.checked) {
      const repositoryId = +data.source.id;
      const repositoryWithHook = this.repositoriesWithHook.find(x => x.repository.id === repositoryId);
      this.webhookApiService.apiGithubWebhooksPost({ repositoryId: repositoryId, access_token: this.authService.getAccessToken() })
        .subscribe(() => {
          repositoryWithHook.configured = true;
        });
    }
  }
}
