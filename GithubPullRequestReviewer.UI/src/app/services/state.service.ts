import { Injectable } from '@angular/core';
import { RepositoryModel } from '../models/repository-model';

@Injectable({
  providedIn: 'root'
})
export class StateService {

  public repositories: RepositoryModel[] = [];

  constructor() { }
}
