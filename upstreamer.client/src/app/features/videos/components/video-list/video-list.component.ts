import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../../../shared/components/base/base.component';
import { Video, VideoResponse } from '../models/video.model';
import { MatCardModule } from '@angular/material/card';
import { NgFor } from '@angular/common';
import { Router } from '@angular/router';
import { TruncatePipe } from '../../../../shared/directives/truncate.directive';
import { MatChipsModule } from '@angular/material/chips';
import { PagedRequest } from '../../../../shared/models/paged.model';
import { takeUntil } from 'rxjs';
import { VideosApiService } from '../../services/videos-api.service';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-video-list',
  templateUrl: './video-list.component.html',
  styleUrl: './video-list.component.css',
  standalone: true,
  imports: [
    MatCardModule,
    NgFor,
    TruncatePipe,
    MatChipsModule,
    MatPaginatorModule,
  ],
})
export class VideoListComponent extends BaseComponent implements OnInit {
  dataSource: Video[] = [];
  total: number = 0;
  pagedRequest: PagedRequest = {
    skip: 0,
    take: 15
  };

  constructor(private router: Router, private apiService: VideosApiService) {
    super();
  }

  ngOnInit(): void {
    this.getVideos();
  }

  navigate(id: number) {
    this.router.navigate([`video-details`, id]);
  }

  handlePageChange(event: PageEvent) {
    console.log(event);
    this.pagedRequest.skip = event.pageIndex * event.pageSize;
    this.pagedRequest.take = this.pagedRequest.skip + event.pageSize;

    this.getVideos();
  }

  getVideos(): void {
    this.apiService
      .getPaged(this.pagedRequest)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((result: VideoResponse) => {
        this.dataSource = result.videos;
        this.total = result.total;
      });
  }
}
