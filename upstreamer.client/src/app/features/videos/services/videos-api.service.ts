import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PagedRequest } from '../../../shared/models/paged.model';
import { VideoDetail, VideoResponse } from './../components/models/video.model';
import { CreateVideoRequest } from '../components/models/upload-video.model';

@Injectable({
  providedIn: 'root',
})
export class VideosApiService {
  private apiUrl = 'https://localhost:7276/api'; //TODO: config

  constructor(private http: HttpClient) {}

  get(id: number): Observable<VideoDetail>{
    return this.http.get<VideoDetail>(`${this.apiUrl}/videos/${id}`);
  }

  getPaged(request: PagedRequest): Observable<VideoResponse> {
    return this.http.get<VideoResponse>(`${this.apiUrl}/videos/list`, {params: request});
  }

  upload(request: FormData): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/files`, request);
  }

  create(request: CreateVideoRequest): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/videos`, request);
  }
}
