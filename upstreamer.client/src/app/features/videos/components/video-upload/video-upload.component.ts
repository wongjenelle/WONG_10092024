import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../../../shared/components/base/base.component';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterModule } from '@angular/router';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { CreateVideoForm, CreateVideoResponse, UploadResult } from '../models/upload-video.model';
import { CreateFormComponent } from './create-form/create-form.component';
import { UploaderComponent } from './uploader/uploader.component';
import { switchMap, takeUntil } from 'rxjs';
import { VideosApiService } from '../../services/videos-api.service';

@Component({
  selector: 'app-video-upload',
  templateUrl: './video-upload.component.html',
  styleUrl: './video-upload.component.css',
  standalone: true,
  imports: [
    CreateFormComponent,
    UploaderComponent,
    MatButtonModule,
    MatSnackBarModule,
    RouterModule,
  ],
})
export class VideoUploadComponent extends BaseComponent implements OnInit {
  file: File | null = null;
  createRequest: FormGroup<CreateVideoForm>;

  constructor(
    private apiService: VideosApiService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private router: Router
  ) {
    super();

    this.createRequest = this.fb.group<CreateVideoForm>({
      title: this.fb.control('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      description: this.fb.control(null),
      category: this.fb.control('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      filePath: this.fb.control(null),
    });
  }

  ngOnInit(): void {}

  setFile(file: File | null) {
    this.file = file;
  }

  cancel() {
    this.router.navigateByUrl('/video-list');
  }

  submit() {
    if (this.file == null) return;

    const formData = new FormData();
    formData.append('file', this.file);

    // todo: move to video service
    this.apiService
      .upload(formData)
      .pipe(
        switchMap((res: UploadResult) => {
          this.createRequest.value.filePath = res.filePath;
          return this.apiService.create(this.createRequest.value);
        }),
        takeUntil(this.unsubscribe$)
      )
      .subscribe({
        next: (result: CreateVideoResponse) => { 
          this.snackBar.open(
            'Video uploaded successfully!',
            'Close'
          );
          this.router.navigate(['video-details', result.id]); 
        },
        error: (err: HttpErrorResponse) => {
          // TODO: create http request interceptor
          if (err.status == 400) {
            // TODO: display validation errors
            this.snackBar.open(err.message, 'Close');
          } else {
            this.snackBar.open(
              'Something went wrong. Please try again.',
              'Close'
            );
          }
        },
      });
  }
}
