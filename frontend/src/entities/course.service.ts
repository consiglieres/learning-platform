import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICourse } from '../interfaces/courses.interface';

@Injectable()
export class ServiceNameService {
  private readonly _apiUrl = 'http://localhost:3000/courses';

  constructor(private _http: HttpClient) {}

  public getCourses(): Observable<ICourse[]> {
    return this._http.get<ICourse[]>(this._apiUrl);
  }

  public saveCourse(courseData: ICourse): void {
    this._http.post(this._apiUrl, courseData);
  }

  public deleteCourse(courseId: number): Observable<any> {
    return this._http.delete(`${this._apiUrl}/${courseId}`);
  }
}
