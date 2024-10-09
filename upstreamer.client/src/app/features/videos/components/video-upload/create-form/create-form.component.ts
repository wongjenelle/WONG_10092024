import { Component, Input, OnInit } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { BaseComponent } from '../../../../../shared/components/base/base.component';
import { CreateVideoForm } from '../../models/upload-video.model';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-create-form',
  templateUrl: './create-form.component.html',
  styles: '',
  standalone: true,
  imports: [NgIf, MatInputModule, MatFormFieldModule, ReactiveFormsModule],
})
export class CreateFormComponent extends BaseComponent implements OnInit {
  @Input() videoForm!: FormGroup<CreateVideoForm>;

  constructor() {
    super();
  }

  ngOnInit(): void {}
}
