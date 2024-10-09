import { Component, inject, OnInit } from '@angular/core';
import { BaseComponent } from '../../../../shared/components/base/base.component';
import { NgIf } from '@angular/common';
import { HttpErrorResponse, HttpEventType } from '@angular/common/http';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {FormBuilder, FormsModule, Validators} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterModule } from '@angular/router';
import { MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import { CreateVideoForm } from '../models/upload-video.model';
import { CreateFormComponent } from './create-form/create-form.component';
import { UploaderComponent } from './uploader/uploader.component';
import { VideosApiService } from '../../videos-api.service';

@Component({
  selector: 'app-video-upload',
  templateUrl: './video-upload.component.html',
  styleUrl: './video-upload.component.css',
  standalone: true,
  imports: [CreateFormComponent, UploaderComponent, NgIf, MatInputModule, MatFormFieldModule, 
    FormsModule, MatButtonModule, MatSnackBarModule, RouterModule]
})
export class VideoUploadComponent extends BaseComponent implements OnInit  {
  private uploadOptions = {reportProgress: true, observe: 'events'};
  private _snackBar = inject(MatSnackBar);

  private fb = inject(FormBuilder);
  createRequest = this.fb.group<CreateVideoForm>({
    title: this.fb.control('', { nonNullable: true, validators: [Validators.required]}),
    description: this.fb.control(null),
    category: this.fb.control('', { nonNullable: true, validators: [Validators.required]}),
    filePath: this.fb.control(null),
    displayFileName: this.fb.control(null)
  });

  isValid = false; //TODO: use isValid once form is done
  uploadedFile: FormData | null = null;

  constructor(private apiService: VideosApiService, 
    private router: Router)  {
    super();
  }

  ngOnInit(): void {
  }

  cancel(){
    this.router.navigateByUrl('/videos');
  }
  
  submit(){
    // todo: create after upload
    this.apiService.upload(this.uploadedFile, this.uploadOptions)
      .subscribe({
        next: (event) => {
          if (event.type === HttpEventType.Response) {
            this._snackBar.open("Video uploaded successfully. Please wait.", "Close");
          }
      },
      error: (err: HttpErrorResponse) => {
        // TODO: create http request interceptor 
        if (err.status == 400){
          this._snackBar.open(err.message, "Close");
        }
        else {
          this._snackBar.open("Something went wrong. Please try again.", "Close");
        }      
      }
    });
  }
}
