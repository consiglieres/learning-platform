import { Injectable } from '@angular/core';
import {map, Observable} from 'rxjs';
import {ICourse} from '../interfaces/courses.interface';
import {CourseService} from './course.service';

@Injectable({
  providedIn: 'root',
})
export class FilterService {

  /*private courses$: Observable<ICourse[]>;*/

  constructor() {
  }

  private filters(filtersData: Object) {
    /*this.courses$.pipe(map(result => {

    }));*/

  }
}
