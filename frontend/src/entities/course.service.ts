import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {
  ICourseFull,
  ICourseCategory,
  ICourseDraft,
  ICourseUpdate,
  IModerationComment,
  ICourse
} from '../interfaces/courses.interface';

const BASE_URL = 'http://localhost:5172/api/v1/V1Courses';

@Injectable({ providedIn: 'root' })
export class CourseService {
  private readonly _http = inject(HttpClient);

  public createDraft(data: ICourseDraft): Observable<ICourseFull> {
    return this._http.post<ICourseFull>(`${BASE_URL}/draft`, data);
  }

  public getCourses(): Observable<ICourse[]> {
    // Заглушка; позже заменить на реальный эндпоинт
    return of([]);
  }

  public getCourseLastVersion(courseId: string): Observable<ICourseFull> {
    return this._http.get<ICourseFull>(`${BASE_URL}/${courseId}/last`);
  }

  public getCategories(): Observable<ICourseCategory[]> {
    return this._http.get<ICourseCategory[]>(`${BASE_URL}/categories`);
  }

  public getCourseVersion(courseId: string, version: number): Observable<ICourseFull> {
    return this._http.get<ICourseFull>(`${BASE_URL}/${courseId}/version/${version}`);
  }

  public updateCourse(courseId: string, data: ICourseUpdate): Observable<void> {
    return this._http.put<void>(`${BASE_URL}/${courseId}`, data);
  }

  public deleteCourse(courseId: string): Observable<void> {
    return this._http.delete<void>(`${BASE_URL}/${courseId}`);
  }

  public submitForModeration(courseId: string): Observable<void> {
    return this._http.post<void>(`${BASE_URL}/${courseId}/submit`, {});
  }

  public approveCourse(courseId: string, comment?: IModerationComment): Observable<void> {
    return this._http.post<void>(`${BASE_URL}/${courseId}/approve`, comment || {});
  }

  public rejectCourse(courseId: string, comment: IModerationComment): Observable<void> {
    return this._http.post<void>(`${BASE_URL}/${courseId}/reject`, comment);
  }

  public unpublishCourse(courseId: string): Observable<void> {
    return this._http.post<void>(`${BASE_URL}/${courseId}/unpublish`, {});
  }

  public archiveCourse(courseId: string): Observable<void> {
    return this._http.post<void>(`${BASE_URL}/${courseId}/archive`, {});
  }

  public restoreCourse(courseId: string): Observable<void> {
    return this._http.post<void>(`${BASE_URL}/${courseId}/restore`, {});
  }

  public getMyCourses(): Observable<ICourse[]> {
    // TODO: заменить на реальный запрос, например:
    // return this._http.get<ICourse[]>(`${BASE_URL}/my`);
    return new Observable(subscriber => {
      subscriber.next([]);
      subscriber.complete();
    });
  }
}
