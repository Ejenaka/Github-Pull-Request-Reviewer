import { Routes } from '@angular/router';
import { AuthComponent } from './components/auth/auth.component';
import { authGuard } from './guards/auth.guard';
import { NavigationComponent } from './components/navigation/navigation.component';
import { RepositoriesComponent } from './components/repositories/repositories.component';
import { PullRequestsComponent } from './components/pull-requests/pull-requests.component';
import { CodeAnalysisComponent } from './components/code-analysis/code-analysis.component';

export const routes: Routes = [
  { path: "auth", component: AuthComponent },
  {
    path: '',
    component: NavigationComponent,
    canActivateChild: [authGuard],
    children: [
      { path: 'repositories', component: RepositoriesComponent, data: { pageTitle: 'Repositories' } },
      { path: 'pull-requests', component: PullRequestsComponent, data: { pageTitle: 'Pull Requests' } },
      { path: 'code-analysis', component: CodeAnalysisComponent, data: { pageTitle: 'Repositories' } },
      { path: '', redirectTo: 'repositories', pathMatch: 'full' },
    ],
  },
  { path: '**', redirectTo: '' },
];
