import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable, take, tap} from 'rxjs';
import {ICourse, ITopic} from '../interfaces/courses.interface';
import {FilterService} from './filter.service';

@Injectable({providedIn: 'root'})
export class CourseService {
  private readonly _apiUrl = 'http://localhost:3000/courses';
  public _courseSubject = new BehaviorSubject<ICourse[]>([]);

  constructor(private _http: HttpClient, private _filterService: FilterService) {}

  public getCourses(): Observable<ICourse[]> {
    return this._http.get<ICourse[]>(this._apiUrl).pipe(
      take(1),
      // кладём данные в фильтр-сервис
      tap(courses => this._courseSubject.next(courses))
    );
  }

  public saveCourse(courseData: ICourse): void {

    if(courseData.title) {}
    this._http.post(this._apiUrl, courseData);
  }

  public deleteCourse(courseId: number): Observable<any> {
    return this._http.delete(`${this._apiUrl}/${courseId}`);
  }

  public getCoursesContentTopic(): Observable<ITopic[]>{
    return this._http.get<ITopic[]>(this._apiUrl)
  }


}
