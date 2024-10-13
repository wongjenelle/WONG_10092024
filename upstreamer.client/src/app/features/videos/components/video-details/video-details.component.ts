import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../../../shared/components/base/base.component';
import { ActivatedRoute, Params } from '@angular/router';
import { VideoDetail } from '../models/video.model';
import { switchMap, takeUntil } from 'rxjs';
import { VideosApiService } from '../../services/videos-api.service';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-video-details',
  templateUrl: './video-details.component.html',
  styleUrl: './video-details.component.css',
  standalone: true,
  imports: [NgIf]
})
export class VideoDetailsComponent extends BaseComponent implements OnInit {
  videoDetails: VideoDetail = {
    id: 0,
    title: '',
    description: null,
    category: '',
    filePath: '',
  };

  constructor(
    private route: ActivatedRoute,
    private videoService: VideosApiService
  ) {
    super();
  }

  ngOnInit() {
    this.route.params
      .pipe(
        switchMap((params: Params) => {
          return this.getVideo(params['id']);
        }),
        takeUntil(this.unsubscribe$)
      )
      .subscribe((res: VideoDetail) => {
        this.videoDetails = res;
      });
  }

  getVideo(id: number) {
    return this.videoService.get(id);
  } 
}
