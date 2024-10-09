import { Component } from '@angular/core';
import { OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-base',
  standalone: true,
  template: '',
  styles: '',
})
export abstract class BaseComponent implements OnDestroy {
  protected unsubscribe$ = new Subject();

  ngOnDestroy() {
    this.unsubscribe$.next(null);
    this.unsubscribe$.unsubscribe();
  }
}
