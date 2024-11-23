import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Route, Router, RouterOutlet } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { Subscription } from 'rxjs';
import { LocalStorageService } from '../../services/local-storage.service';
import { TOKEN_STORAGE_KEY } from '../../common/constants';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { UserService } from '../../api/pull-request/services';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [
    MatIconModule,
    MatButtonModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit, OnDestroy {
  private paramMapSub$: Subscription;

  isLoading: boolean;

  constructor(
    private readonly userApiService: UserService,
    private readonly localStorageService: LocalStorageService,
    private readonly router: Router,
    private readonly activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.paramMapSub$ = this.activatedRoute.queryParamMap.subscribe(param => {
      const code = param.get('code');
      if (code) {
        this.userApiService.apiUsersAuthTokenGet$Plain({ code: code, access_token: '' }).subscribe(token => {
          this.localStorageService.saveItem({ key: TOKEN_STORAGE_KEY, value: token });
        });
      }
    });
  }

  ngOnDestroy() {
    this.paramMapSub$.unsubscribe();
  }

  loginWithGitHub() {
    this.isLoading = true;
    this.userApiService.apiUsersAuthUrlGet$Plain({ access_token: '' }).subscribe({
      next: (url) => {
        this.router.navigateByUrl(url);
      },
      complete: () => this.isLoading = false,
      error: () => this.isLoading = false
    });
  }
}
