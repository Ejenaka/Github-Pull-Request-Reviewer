import { Injectable } from '@angular/core';
import { LocalStorageService } from './local-storage.service';
import { TOKEN_STORAGE_KEY } from '../common/constants';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private readonly localStorageService: LocalStorageService) { }

  getAccessToken(): string {
    return this.localStorageService.getItem(TOKEN_STORAGE_KEY).value;
  }

  saveAccessToken(token: string) {
    this.localStorageService.saveItem({ key: TOKEN_STORAGE_KEY, value: token });
  }

  isUserLoggedIn(): boolean {
    return !!this.getAccessToken();
  }
}
