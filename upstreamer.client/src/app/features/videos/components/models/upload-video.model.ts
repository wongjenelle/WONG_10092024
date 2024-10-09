import { FormControl } from '@angular/forms';

export type CreateVideoForm = {
  title: FormControl<string>;
  description: FormControl<string | null>;
  category: FormControl<string>;
  filePath: FormControl<string | null>;
};
