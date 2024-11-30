import { CommonModule } from '@angular/common';
import { Component, computed, effect, OnInit, signal } from '@angular/core';
import { MatSelectModule } from '@angular/material/select';
import { ApiService } from '../../services/api.service';
import { PullRequest, PullRequestFile, Recommendation, Repository } from '../../api/pull-request/models';
import { forkJoin, Observable, of } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { HighlightAuto } from 'ngx-highlightjs';
import { HighlightPlusModule } from 'ngx-highlightjs/plus';
import { HighlightLineNumbers } from 'ngx-highlightjs/line-numbers';

@Component({
  selector: 'app-code-analysis',
  standalone: true,
  imports: [
    CommonModule,
    MatSelectModule,
    MatCardModule,
    HighlightPlusModule,
    HighlightAuto,
    HighlightLineNumbers,
  ],
  templateUrl: './code-analysis.component.html',
  styleUrl: './code-analysis.component.scss'
})
export class CodeAnalysisComponent implements OnInit {
  repositories: Repository[] = [];
  pullRequests: PullRequest[] = [];
  recommendations: Recommendation[] = [];
  files: PullRequestFile[] = [];
  fileContents: { [key: string]: Observable<string> } = {};
  selectedRepository = signal<Repository | null>(null);
  selectedPullRequest = signal<PullRequest | null>(null);

  constructor(
    private readonly apiService: ApiService) {

    effect(() => {
      console.log('repo effect')
      const repo = this.selectedRepository();
      console.log(repo);
      if (repo) {
        this.onRepositorySelected(repo.id as number);
      }
    });

    effect(() => {
      console.log('repo & pr effect')
      const repo = this.selectedRepository();
      const pr = this.selectedPullRequest();
      if (repo && pr) {
        this.onPullRequestSelected(repo.id as number, pr.number as number);
      }
    });
  }

  ngOnInit(): void {
    this.apiService.getUserRepositories()
      .subscribe(repos => this.repositories = repos.filter(r => r.configured).map(r => r.repository));
  }

  onRepositorySelected(repoId: number) {
    this.apiService.getPullRequestsByRepository(repoId).subscribe(pullRequests => this.pullRequests = pullRequests);
  }

  onPullRequestSelected(repoId: number, pullRequestNumber: number) {
    forkJoin([this.apiService.getRecommendations(repoId, pullRequestNumber), this.apiService.getPullRequestFiles(repoId, pullRequestNumber)])
      .subscribe(res => {
        this.recommendations = res[0];
        this.files = res[1];
        this.recommendations.forEach(recommendation => {
          this.fileContents[`${recommendation.repositoryId}-${recommendation.fileName}`] = this.getFileContent(recommendation.fileName, recommendation.repositoryId);
        });
      });
  }

  getFileContent(fileName: string, repositoryId: number): Observable<string> {
    const repositoryName = this.repositories.find(r => r.id === repositoryId).name;
    const headRef = this.selectedPullRequest().headRef;
    return this.apiService.getPullRequestFileContent(fileName, headRef, repositoryName);
  }

  getFileContentFromFileContents(repositoryId: number, fileName: string): Observable<string> {
    return this.fileContents[`${repositoryId}-${fileName}`];
  }
}
