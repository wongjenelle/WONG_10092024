import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VideosApiService {
  private apiUrl = 'https://localhost:7276/api'; //TODO: config

  constructor(private http: HttpClient) {

  }

  upload(request: any, options?: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/files`, request, options);
  }
}
