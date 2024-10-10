import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PagedRequest } from '../../shared/models/paged.model';

@Injectable({
  providedIn: 'root',
})
export class VideosApiService {
  private apiUrl = 'https://localhost:7276/api'; //TODO: config

  constructor(private http: HttpClient) {}

  // TODO: change to strongly typed

  getPaged(request: PagedRequest): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/videos`, {params: request});
  }

  upload(request: FormData): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/files`, request);
  }

  create(request: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/videos`, request);
  }
}
