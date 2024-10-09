import { Component, Input, OnInit } from '@angular/core';
import { NgIf } from '@angular/common';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {FormGroup, FormsModule} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { BaseComponent } from '../../../../../shared/components/base/base.component';
import { CreateVideoForm } from '../../models/upload-video.model';

@Component({
  selector: 'app-create-form',
  templateUrl: './create-form.component.html',
  styles: '',
  standalone: true,
  imports: [NgIf, MatInputModule, MatFormFieldModule, FormsModule, MatButtonModule]
})
export class CreateFormComponent extends BaseComponent implements OnInit  {

  constructor()  {
    super();
  }

  ngOnInit(): void {
  }
}
