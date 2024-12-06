import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { PullRequest, Recommendation } from '../../api/pull-request/models';
import { mergeMap, Observable } from 'rxjs';
import { ApiService } from '../../services/api.service';
import { ReviewStatusComponent } from "../shared/review-status/review-status.component";

@Component({
  selector: 'app-pull-requests',
  standalone: true,
  imports: [
    MatCardModule,
    MatButtonToggleModule,
    MatSlideToggleModule,
    CommonModule,
    ReviewStatusComponent
  ],
  templateUrl: './pull-requests.component.html',
  styleUrl: './pull-requests.component.scss'
})
export class PullRequestsComponent implements OnInit {
  pullRequests: PullRequest[] = [];

  constructor(private readonly apiService: ApiService) { }

  ngOnInit(): void {
    this.apiService.getUserRepositories()
      .pipe(
        mergeMap(repos => {
          const reposId = repos.filter(r => r.configured).map(r => r.repository.id) as number[];
          return this.apiService.getPullRequestsByRepositories(reposId);
        }),
      ).subscribe(pullRequests => this.pullRequests.push(...pullRequests));
  }

  getRecommendationsForPullRequest(repoId: number, prNumber: number): Observable<Recommendation[]> {
    return this.apiService.getRecommendations(repoId, prNumber);
  }
}
