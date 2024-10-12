import { FormControl } from '@angular/forms';

export type CreateVideoRequest = {
  title: string;
  description: string;
  category: string;
  filePath: string;
};

export type CreateVideoForm = {
  title: FormControl<string>;
  description: FormControl<string>;
  category: FormControl<string>;
  filePath: FormControl<string>;
};

export type CreateVideoResponse = {
  id: number;
}

export type UploadResult = {
  filePath: string;
}