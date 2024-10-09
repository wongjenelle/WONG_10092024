import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class VideosApiService {
  private apiUrl = 'https://localhost:7276/api'; //TODO: config

  constructor(private http: HttpClient) {}

  // TODO: chawnge to strongly typed

  upload(request: FormData): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/files`, request);
  }

  create(request: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/videos`, request);
  }
}
