import { Component, Input, OnInit } from '@angular/core';
import { Recommendation } from '../../../api/pull-request/models';
import { CommonModule } from '@angular/common';
import { MatBadgeModule } from '@angular/material/badge';
import { MatIconModule } from '@angular/material/icon';
import { RecommendationType } from './recommendation-type.enum';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-review-status',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatBadgeModule,
  ],
  templateUrl: './review-status.component.html',
  styleUrl: './review-status.component.scss'
})
export class ReviewStatusComponent implements OnInit {
  @Input('recommendations$') recommendations$: Observable<Recommendation[]>;
  @Input() set singleRecommendation(recommendation: Recommendation) {
    this.recommendations.push(recommendation);
  }

  recommendations: Recommendation[] = [];

  ngOnInit(): void {
    if (this.recommendations$) {
      this.recommendations$.subscribe(recommendations => {
        this.recommendations = recommendations;
      });
    }
  }

  isIssuePresented(): boolean {
    return this.isRecommendationTypePresented(RecommendationType.Issue);
  }

  isVulnerabilityPresented(): boolean {
    return this.isRecommendationTypePresented(RecommendationType.Vulnerability);
  }

  isOptimizationPresented(): boolean {
    return this.isRecommendationTypePresented(RecommendationType.Optimization);
  }

  isEnhancmentPresented(): boolean {
    return this.isRecommendationTypePresented(RecommendationType.Enhancement);
  }

  isBestPractisePresented(): boolean {
    return this.isRecommendationTypePresented(RecommendationType.BestPractice);
  }

  getIssuesCount(): number {
    return this.getfilteredRecommendations(RecommendationType.Issue)?.length;
  }

  getVulnerabilitiesCount(): number {
    return this.getfilteredRecommendations(RecommendationType.Vulnerability)?.length;
  }

  getOptimizationAndEnhancmentAndBestPractiseCount(): number {
    return this.getfilteredRecommendations(RecommendationType.Optimization)?.length
      + this.getfilteredRecommendations(RecommendationType.Enhancement)?.length
      + this.getfilteredRecommendations(RecommendationType.BestPractice)?.length;
  }

  private isRecommendationTypePresented(recommendationType: RecommendationType): boolean {
    return this.recommendations?.some(r => r.type === recommendationType as number);
  }

  private getfilteredRecommendations(recommendationType: RecommendationType): Recommendation[] {
    return this.recommendations?.filter(r => r.type === recommendationType as number);
  }
}
