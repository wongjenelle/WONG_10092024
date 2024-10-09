import { Component, Input, OnInit } from '@angular/core';
import sanitize from 'sanitize-filename';
import { MatButtonModule } from '@angular/material/button';
import { VideosConstants } from '../../constants/videos.constants';
import { BaseComponent } from '../../../../../shared/components/base/base.component';

@Component({
  selector: 'app-uploader',
  templateUrl: './uploader.component.html',
  styles: '',
  standalone: true,
  imports: [MatButtonModule]
})
export class UploaderComponent extends BaseComponent implements OnInit  {
  @Input() formData?: FormData | null = null;
  private fileSize = 0;

  message = "";
  fileName = "";
  extensions: string = VideosConstants.ALLOWED_EXTENSIONS.join(",");

  constructor()  {
    super();
  }

  ngOnInit(): void {
  }

  resetUploadDetails(){
    this.message = "";
    this.fileName = "";
    this.fileSize = 0;
    this.formData = undefined;
  }

  onFileSelected(event: Event) {
    this.resetUploadDetails();
    let eventTarget = (event.target as HTMLInputElement);
    let file = (eventTarget.files as FileList)[0];

    if (file) {
      this.fileSize = Math.round((file.size / 1024) * 100) / 100; // KB
      if(this.fileSize > VideosConstants.MAX_SIZE_KB){
        this.message = "File size exceeds the maximum limit! Maximum size: 100MB.";
        this.fileSize = 0;
        return;
      }

      this.fileName = sanitize(file.name);
      this.formData = new FormData();
      this.formData.append('file', file); 
    }
  }
}
