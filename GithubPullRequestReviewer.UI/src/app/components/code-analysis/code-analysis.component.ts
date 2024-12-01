import { CommonModule } from '@angular/common';
import { Component, effect, OnInit, signal } from '@angular/core';
import { MatFormField, MatSelectModule } from '@angular/material/select';
import { ApiService } from '../../services/api.service';
import { PullRequest, PullRequestFile, Recommendation, Repository, Comment } from '../../api/pull-request/models';
import { forkJoin, map, Observable, of, switchMap, tap } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { HighlightAuto } from 'ngx-highlightjs';
import { HighlightPlusModule } from 'ngx-highlightjs/plus';
import { HighlightLineNumbers } from 'ngx-highlightjs/line-numbers';
import { Buffer } from "buffer";
import { MatExpansionModule } from '@angular/material/expansion';
import { MatButton, MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import 'highlight.js/styles/vs.min.css';


@Component({
  selector: 'app-code-analysis',
  standalone: true,
  imports: [
    CommonModule,
    MatSelectModule,
    MatCardModule,
    MatExpansionModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
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
  // commentsForRecommendation: Comment[] = [];
  fileContents: { [key: string]: Observable<string> } = {};
  commentsForRecommendation: { [key: string]: Comment[] } = {};
  selectedRepository = signal<Repository | null>(null);
  selectedPullRequest = signal<PullRequest | null>(null);

  constructor(
    private readonly apiService: ApiService) {

    effect(() => {
      const repo = this.selectedRepository();
      if (repo) {
        this.onRepositorySelected(repo.id as number);
      }
    });

    effect(() => {
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
          const fileContent = this.getFileContent(recommendation.fileName, this.selectedRepository().name).pipe(
            map(fileContent => {
              const splited = fileContent.split('\n');
              const section = splited.splice(recommendation.codeLines[0] - 1, recommendation.codeLines[1]);
              return section.join('\n');
            }),
          );

          this.fileContents[`${recommendation.repositoryId}-${recommendation.fileName}`] = fileContent;
        });
      });
  }

  onRecommendationOpened(recommendationId: number) {
    this.apiService.getCommentsForRecommendation(recommendationId).subscribe(comments => {
      this.commentsForRecommendation[recommendationId] = comments;
    });
  }

  getFileContent(fileName: string, repositoryName: string): Observable<string> {
    return this.apiService.getFileContent({
      repositoryName: repositoryName,
      filePath: fileName,
      headRef: this.selectedPullRequest().headRef
    }).pipe(
      map(encodedContent => Buffer.from(encodedContent, 'base64').toString())
    );
  }

  getFileContentFromFileContents(repositoryId: number, fileName: string): Observable<string> {
    return this.fileContents[`${repositoryId}-${fileName}`];
  }
}
