import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  getItem(key: string): StorageItem {
    return { key: key, value: localStorage.getItem(key) };
  }

  saveItem(item: StorageItem) {
    localStorage.setItem(item.key, item.value);
  }

  removeItem(key: string) {
    localStorage.removeItem(key);
  }
}

export interface StorageItem {
  key: string,
  value: any
}