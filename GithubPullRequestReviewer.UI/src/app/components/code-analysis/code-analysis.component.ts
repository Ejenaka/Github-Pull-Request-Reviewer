import { CommonModule } from '@angular/common';
import { Component, effect, OnInit, signal, viewChild } from '@angular/core';
import { MatSelectModule } from '@angular/material/select';
import { ApiService } from '../../services/api.service';
import { PullRequest, PullRequestFile, Recommendation, Repository, Comment, User, RecommendationStatus } from '../../api/pull-request/models';
import { forkJoin, map, Observable, of, switchMap, tap } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { HighlightAuto } from 'ngx-highlightjs';
import { HighlightPlusModule } from 'ngx-highlightjs/plus';
import { HighlightLineNumbers } from 'ngx-highlightjs/line-numbers';
import { Buffer } from "buffer";
import { MatAccordion, MatExpansionModule } from '@angular/material/expansion';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';

import 'highlight.js/styles/vs.min.css';
import { ReviewStatusComponent } from "../shared/review-status/review-status.component";

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
    MatDividerModule,
    MatFormFieldModule,
    MatIconModule,
    HighlightPlusModule,
    HighlightAuto,
    HighlightLineNumbers,
    ReviewStatusComponent
  ],
  templateUrl: './code-analysis.component.html',
  styleUrl: './code-analysis.component.scss'
})
export class CodeAnalysisComponent implements OnInit {
  accordion = viewChild.required(MatAccordion);
  selectedRepository = signal<Repository | null>(null);
  selectedPullRequest = signal<PullRequest | null>(null);
  currentUser: User;
  repositories: Repository[] = [];
  pullRequests: PullRequest[] = [];
  recommendations: Recommendation[] = [];
  files: PullRequestFile[] = [];
  fileContents: { [key: string]: Observable<string> } = {};
  commentsForRecommendation: { [key: number]: Comment[] } = {};
  userComments: { [key: number]: string } = {};

  constructor(private readonly apiService: ApiService) {
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

    this.apiService.getAuthenticatedUser()
      .subscribe(user => this.currentUser = user);
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

  onRecommendationResolved(recommendation: Recommendation) {
    recommendation.status = 1;
    this.apiService.updateRecommendationStatus(recommendation).subscribe(() => this.accordion().closeAll());
  }

  onCommentInputChanged(event: Event, recommendationId: number) {
    const value = (event.target as HTMLTextAreaElement).value;
    this.userComments[recommendationId] = value;
    console.log(this.userComments[recommendationId]);
  }

  onCommentSubmitButtonClick(recommendationId: number) {
    const userComment = this.userComments[recommendationId];
    this.commentsForRecommendation[recommendationId].push({ recommendationId: recommendationId, text: userComment, isFromUser: true });

    this.apiService.createCommentForRecommendation({
      recommendationId: recommendationId,
      isFromUser: true,
      text: userComment
    }).subscribe(responseComment => {
      this.commentsForRecommendation[recommendationId].push(responseComment);
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
