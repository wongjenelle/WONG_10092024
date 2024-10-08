import { Component } from '@angular/core';
import { OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-base',
  standalone: true,
  template: '',
  styles: ''
})

export abstract class BaseComponent implements OnDestroy {
  public ngDestroyed$ = new Subject();

  ngOnDestroy() {
    this.ngDestroyed$.next(null);
    this.ngDestroyed$.unsubscribe();
  }
}