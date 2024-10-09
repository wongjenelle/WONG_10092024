  import { Component, OnInit } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { NavigationEnd, Router, RouterModule } from '@angular/router';
import {MatIconModule} from '@angular/material/icon';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { NgIf } from '@angular/common';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrl: './header.component.css',
    standalone: true,
    imports: [MatIconModule, MatButton, RouterModule, NgIf],
})
export class HeaderComponent extends BaseComponent implements OnInit {
    isUploadPage = false;

    constructor(private router: Router){
        super();
    }
    ngOnInit(): void {
        this.router.events.subscribe((val) => {
            if (val instanceof NavigationEnd) {
              if (val.url == '/upload') {
                this.isUploadPage = true;
              } else {
                this.isUploadPage = false;
              }
            }
          });
    }
}
