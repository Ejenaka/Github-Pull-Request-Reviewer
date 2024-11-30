import { Injectable } from '@angular/core';
import { LocalStorageService } from './local-storage.service';
import { TOKEN_STORAGE_KEY } from '../common/constants';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private readonly localStorageService: LocalStorageService,
    private readonly router: Router) { }

  getAccessToken(): string {
    return this.localStorageService.getItem(TOKEN_STORAGE_KEY).value;
  }

  saveAccessToken(token: string) {
    this.localStorageService.saveItem({ key: TOKEN_STORAGE_KEY, value: token });
  }

  isUserLoggedIn(): boolean {
    const token = this.getAccessToken();
    return !!token && token !== null && token !== '' && token !== 'null';
  }

  logOut() {
    this.localStorageService.removeItem(TOKEN_STORAGE_KEY);
    this.router.navigate(['auth'], { replaceUrl: true });
  }
}
