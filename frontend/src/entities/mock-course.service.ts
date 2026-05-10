import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { delay } from 'rxjs/operators';
import {
  ICourseFull,
  ICourseCategory,
  ICourseDraft,
  ICourseUpdate,
  IModerationComment,
  ICourse
} from '../interfaces/courses.interface';

@Injectable()
export class MockCourseService {
  // Моковые курсы для профиля
  private mockCourses: ICourse[] = [
    {
      id: '1',
      title: 'Angular 21 для профессионалов',
      description: 'Глубокое погружение в Signals и новейший синтаксис.',
      status: 1,
      categories: [{ type: 'frontend', value: 'Angular' }]
    },
    {
      id: '2',
      title: 'RxJS и реактивное программирование',
      description: 'От Observables до Signals — плавный переход.',
      status: 1,
      categories: [{ type: 'frontend', value: 'RxJS' }]
    },
    {
      id: '3',
      title: 'Архитектура больших Angular-приложений',
      description: 'Feature-Sliced Design, DI и монорепозиторий.',
      status: 1,
      categories: [{ type: 'architecture', value: 'FSD' }]
    }
  ];

  // Эмуляция успешного создания черновика
  public createDraft(data: ICourseDraft): Observable<ICourseFull> {
    return of({
      title: data.title,
      description: data.description,
      categories: data.categories,
      modules: [],
      introductionPage: null!,
      moderationComment: null,
      submittedForModerationAt: null,
      submittedBy: null!,
      publishedAt: null,
      publishedBy: null!,
      status: 0,
      version: { order: 1, tag: null },
      id: 'mock-' + Date.now(),
      createdAt: new Date().toISOString(),
      createdBy: null!,
      updatedAt: null,
      updatedBy: null!,
      deletedAt: null,
      deletedBy: null!
    } as ICourseFull).pipe(delay(300));
  }

  public getCourseLastVersion(courseId: string): Observable<void> {
    return of(undefined).pipe(delay(300)); // заглушка
  }

  public getCategories(): Observable<ICourseCategory[]> {
    return of([
      { type: 'frontend', value: 'Angular' },
      { type: 'backend', value: 'NestJS' }
    ]).pipe(delay(300));
  }

  public getCourseVersion(courseId: string, version: number): Observable<void> {
    return of(undefined).pipe(delay(300));
  }

  public updateCourse(courseId: string, data: ICourseUpdate): Observable<void> {
    return of(undefined).pipe(delay(300));
  }

  public deleteCourse(courseId: string): Observable<void> {
    return of(undefined).pipe(delay(300));
  }

  public submitForModeration(courseId: string): Observable<void> {
    return of(undefined).pipe(delay(300));
  }

  public approveCourse(courseId: string, comment?: IModerationComment): Observable<void> {
    return of(undefined).pipe(delay(300));
  }

  public rejectCourse(courseId: string, comment: IModerationComment): Observable<void> {
    return of(undefined).pipe(delay(300));
  }

  public unpublishCourse(courseId: string): Observable<void> {
    return of(undefined).pipe(delay(300));
  }

  public archiveCourse(courseId: string): Observable<void> {
    return of(undefined).pipe(delay(300));
  }

  public restoreCourse(courseId: string): Observable<void> {
    return of(undefined).pipe(delay(300));
  }

  public getMyCourses(): Observable<ICourse[]> {
    return of([...this.mockCourses]).pipe(delay(400));
  }
}
