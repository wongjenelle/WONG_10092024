import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../../../shared/components/base/base.component';
import { NgIf } from '@angular/common';
import { HttpClient, HttpErrorResponse, HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-video-upload',
  templateUrl: './video-upload.component.html',
  styleUrl: './video-upload.component.css',
  standalone: true,
  imports: [NgIf]
})
export class VideoUploadComponent extends BaseComponent implements OnInit  {
  outputBoxVisible = false;
  uploadResult = '';
  fileName = '';
  fileSize = '';
  uploadStatus: number | undefined;
  progress: number = 0;
  // TODO: allowed extensions
  // todo: security

  constructor(private http: HttpClient)  {
    super();
  }

  ngOnInit(): void {
  }

  onFileSelected(event: Event) {
    this.outputBoxVisible = false;
    this.uploadResult = '';
    this.fileName = '';
    this.fileSize = '';
    this.uploadStatus = undefined;
    let eventTarget = (event.target as HTMLInputElement);
    let file = (eventTarget.files as FileList)[0];

    if (file) {
      this.fileName = file.name; // TODO: sanitize
      this.fileSize = `${(file.size / 1024).toFixed(2)} KB`;
      this.outputBoxVisible = true;

      const formData = new FormData();
      formData.append('file', file);

      this.http.post('https://localhost:7276/api/videos/upload', formData, {reportProgress: true, observe: 'events'})
      .subscribe({
        next: (event) => {
        // if (event.type === HttpEventType.UploadProgress)
        //   this.progress = Math.round(100 * event.loaded / event.total);
        // else if (event.type === HttpEventType.Response) {
        //   this.onUploadFinished.emit(event.body);
        // }
      },
      error: (err: HttpErrorResponse) => console.log(err)
    });
    }
  }
}
