import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../../../shared/components/base/base.component';
import { Video } from '../models/video.model';
import { MatCardModule } from '@angular/material/card';
import { NgFor } from '@angular/common';
import { Router } from '@angular/router';
import { TruncatePipe } from '../../../../shared/directives/truncate.directive';
import { MatChipsModule } from '@angular/material/chips';

@Component({
  selector: 'app-video-list',
  templateUrl: './video-list.component.html',
  styleUrl: './video-list.component.css',
  standalone: true,
  imports: [MatCardModule, NgFor, TruncatePipe, MatChipsModule],
})
export class VideoListComponent extends BaseComponent implements OnInit {
  dataSource: Video[] = [];

  constructor(private router: Router) {
    super();
  }

  ngOnInit(): void {
    this.dataSource = this.getVideos();
  }

  navigate(id: number) {
    this.router.navigate([`video-details`, id]);
  }

  getVideos(): Video[] {
    return [
      <Video>{
        id: 1,
        title: 'My Video',
        category: 'Simple',
        description:
          'All behavior is based on the expected behavior of the JavaScript API Array.prototype.slice() and String.prototype.slice(). When operating on an Array, the returned Array is always a copy even when all the elements are being returned.',
      },
      <Video>{
        id: 2,
        title: 'Second Video!',
        category: 'Simple',
        description: 'My second video now!',
      },
      <Video>{
        id: 3,
        title: 'Bird Watching',
        category: 'Bird',
        description: 'A video for my hobby.',
      },
    ];
  }
}
