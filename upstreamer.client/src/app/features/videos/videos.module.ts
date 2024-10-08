import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VideosRoutingModule } from './videos-routing.module';
import { VideoListComponent } from './components/video-list/video-list.component';
import { MatTableModule } from '@angular/material/table';
import { VideoUploadComponent } from './video-upload/video-upload.component';

@NgModule({
  declarations: [
    VideoListComponent,
    VideoUploadComponent
  ],
  imports: [
    CommonModule,
    MatTableModule,
    VideosRoutingModule
  ]
})
export class VideosModule { }
