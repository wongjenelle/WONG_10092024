import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../../../shared/components/base/base.component';
import { ActivatedRoute, Params } from '@angular/router';
import { VideoDetail } from '../models/video.model';
import { of, switchMap, takeUntil } from 'rxjs';

@Component({
  selector: 'app-video-details',
  templateUrl: './video-details.component.html',
  styleUrl: './video-details.component.css',
  standalone: true,
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
    private route: ActivatedRoute
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
    let vid = <VideoDetail>{
      id: id,
      title: 'My Video',
      category: 'Personal',
      filePath: 'http://localhost:8080\\Upload\\1728537578.mp4',
    };

    return of(vid); // todo: replace with api
  }
}
