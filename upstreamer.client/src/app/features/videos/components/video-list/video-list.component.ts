import { Component } from '@angular/core';

@Component({
    selector: 'app-video-list',
    templateUrl: './video-list.component.html',
    styleUrl: './video-list.component.css',
    standalone: true
})

export class VideoListComponent {
  dataSource : any[] = [
    {title: 'Hydrogen', description: 'H'},
    {title: 'Hydrogen', description: 'H'},
    {title: 'Hydrogen', description: 'H'},
    {title: 'Hydrogen', description: 'H'},
    {title: 'Hydrogen', description: 'H'}
  ];

}
