import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VideoListComponent } from './features/videos/components/video-list/video-list.component';
import { VideoUploadComponent } from './features/videos/components/video-upload/video-upload.component';
import { VideoDetailsComponent } from './features/videos/components/video-details/video-details.component';

const routes: Routes = [
  { path: 'videos', component: VideoListComponent },
  { path: 'videos/details', component: VideoDetailsComponent },
  { path: 'upload', component: VideoUploadComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
