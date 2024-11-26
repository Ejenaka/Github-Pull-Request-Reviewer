import { Routes } from '@angular/router';
import { AuthComponent } from './components/auth/auth.component';
import { AppComponent } from './app.component';
import { authGuard } from './guards/auth.guard';
import { HeaderComponent } from './components/header/header.component';
import { NavigationComponent } from './components/navigation/navigation.component';
import { RepositoriesComponent } from './components/repositories/repositories.component';
import { PullRequestsComponent } from './components/pull-requests/pull-requests.component';
import { CodeAnalysisComponent } from './components/code-analysis/code-analysis.component';

export const routes: Routes = [
  { path: "auth", component: AuthComponent },
  {
    path: '',
    component: AppComponent,
    canActivateChild: [authGuard],
    children: [
      { path: '', component: HeaderComponent, outlet: 'header' },
      {
        path: '',
        component: NavigationComponent,
        children: [
          { path: 'repositories', component: RepositoriesComponent },
          { path: 'pull-requests', component: PullRequestsComponent },
          { path: 'code-analysis', component: CodeAnalysisComponent },
        ]
      },
    ]
  },
  { path: '**', redirectTo: '' }
];
