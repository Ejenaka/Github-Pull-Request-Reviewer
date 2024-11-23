import { Injectable } from '@angular/core';
import { RepositoryState } from '../models/repository-state';

@Injectable({
  providedIn: 'root'
})
export class StateService {

  public repositories: RepositoryState[] = [];

  constructor() { }
}
