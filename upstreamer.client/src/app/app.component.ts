import { Component, OnInit } from '@angular/core';
import { NgIf, NgFor } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './core/components/header/header.component';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    standalone: true,
    imports: [
        NgIf,
        NgFor,
        RouterOutlet,
        HeaderComponent
    ],
})
export class AppComponent implements OnInit {

  constructor() {}

  ngOnInit() {
  }

  title = 'UpStreamer';
}
