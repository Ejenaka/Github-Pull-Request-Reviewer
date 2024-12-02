import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ActivatedRoute, NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { HeaderComponent } from "../header/header.component";

@Component({
  selector: 'app-navigation',
  standalone: true,
  imports: [
    RouterOutlet,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    MatToolbarModule,
    HeaderComponent
  ],
  templateUrl: './navigation.component.html',
  styleUrl: './navigation.component.scss'
})
export class NavigationComponent implements OnInit {
  pageTitle: string;

  constructor(
    private readonly router: Router,
    private readonly activatedRouter: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRouter.data.subscribe(data => {
      this.pageTitle = data['pageTitle'];
    })
  }

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
