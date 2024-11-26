import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-navigation',
  standalone: true,
  imports: [
    RouterOutlet,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    MatToolbarModule
  ],
  templateUrl: './navigation.component.html',
  styleUrl: './navigation.component.scss'
})
export class NavigationComponent {
  pageTitle: string;

  constructor(private readonly router: Router) { }

  navigateToPage(pagePath: string) {
    if (pagePath === 'repositories') {
      this.pageTitle = 'Repositories';
      this.router.navigate(['repositories']);
    } else if (pagePath === 'pull-requests') {
      this.pageTitle = 'Pull Requests';
      this.router.navigate(['pull-requests']);
    } else if (pagePath === 'code-analysis') {
      this.pageTitle = 'Code Analysis';
      this.router.navigate(['code-analysis']);
    }
  }
}
