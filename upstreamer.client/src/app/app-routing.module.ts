import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VideoListComponent } from './features/videos/components/video-list/video-list.component';
import { VideoUploadComponent } from './features/videos/components/video-upload/video-upload.component';
import { VideoDetailsComponent } from './features/videos/components/video-details/video-details.component';

const routes: Routes = [
  { path: '', redirectTo: 'videos', pathMatch: 'full'},
  { path: 'videos', component: VideoListComponent },
  { path: 'video-details/:id', component: VideoDetailsComponent },
  { path: 'upload', component: VideoUploadComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
