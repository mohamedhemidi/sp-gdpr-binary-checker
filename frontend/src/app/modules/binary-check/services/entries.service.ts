import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IResponse } from '../../../shared/types/IResponse';
import { IEntry, IEntryRequest } from '../types/entries';
import { env } from '../../../environments/environment.dev';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class EntriesService {
  constructor(private http: HttpClient) {}

  getEntries = (): Observable<IResponse<IEntry[]>> => {
    return this.http.get<IResponse<IEntry[]>>(env.GetAllEntries);
  };
  checkEntry = (data: IEntryRequest): Observable<IResponse<boolean | null>> => {
    return this.http.post<IResponse<boolean | null>>(env.CheckEntry, data);
  };
}
