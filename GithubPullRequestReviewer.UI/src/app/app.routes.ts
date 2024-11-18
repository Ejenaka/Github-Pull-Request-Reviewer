import { Routes } from '@angular/router';
import { AuthComponent } from './components/auth/auth.component';
import { AppComponent } from './app.component';
import { authGuard } from './guards/auth.guard';
import { HeaderComponent } from './components/header/header.component';
import { NavigationComponent } from './components/navigation/navigation.component';

export const routes: Routes = [
  { path: "auth", component: AuthComponent },
  {
    path: '',
    component: AppComponent,
    canActivateChild: [authGuard],
    children: [
      { path: '', component: HeaderComponent, outlet: 'header' },
      { path: '', component: NavigationComponent }
    ]
  },
  { path: '**', redirectTo: '' }
];
