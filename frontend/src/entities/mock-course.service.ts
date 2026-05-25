import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { delay } from 'rxjs/operators';
import {
  ICourseFull,
  ICourseCategory,
  ICourseDraft,
  ICourseUpdate,
  IModerationComment,
  ICourse,
  ITask
} from '../interfaces/courses.interface';

@Injectable()
export class MockCourseService {
  private mockCourses: ICourse[] = [
    {
      id: '1', title: 'Angular 21 для профессионалов',
      description: 'Глубокое погружение в Signals и новейший синтаксис.',
      status: 1, categories: [{ type: 'frontend', value: 'Angular' }],
      image: 'assets/course-logo.png', duration: 40, tasks: 12, language: 'Angular'
    },
    {
      id: '2', title: 'RxJS и реактивное программирование',
      description: 'От Observables до Signals — плавный переход.',
      status: 1, categories: [{ type: 'frontend', value: 'RxJS' }],
      image: 'assets/course-logo.png', duration: 30, tasks: 10, language: 'RxJS'
    },
    {
      id: '3', title: 'Архитектура больших Angular-приложений',
      description: 'Feature-Sliced Design, DI и монорепозиторий.',
      status: 1, categories: [{ type: 'architecture', value: 'FSD' }],
      image: 'assets/course-logo.png', duration: 20, tasks: 8, language: 'Architecture'
    },
    {
      id: '4', title: 'Python для анализа данных',
      description: 'Pandas, NumPy, визуализация.',
      status: 1, categories: [{ type: 'backend', value: 'Python' }],
      image: 'assets/course-logo.png', duration: 50, tasks: 15, language: 'Python'
    },
    {
      id: '5', title: 'C# и .NET Core',
      description: 'Создание API, Entity Framework.',
      status: 1, categories: [{ type: 'backend', value: 'C#' }],
      image: 'assets/course-logo.png', duration: 45, tasks: 11, language: 'C#'
    },
    {
      id: '6', title: 'DevOps с нуля',
      description: 'Docker, Kubernetes, CI/CD.',
      status: 1, categories: [{ type: 'devops', value: 'DevOps' }],
      image: 'assets/course-logo.png', duration: 35, tasks: 9, language: 'DevOps'
    }
  ];

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
    return of(undefined).pipe(delay(300));
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

  public getCourses(): Observable<ICourse[]> {
    return of([...this.mockCourses]).pipe(delay(400));
  }

  public getMyCourses(): Observable<ICourse[]> {
    return of([...this.mockCourses.slice(0, 3)]).pipe(delay(400));
  }

  public getCourseById(courseId: string): Observable<any> {
    const found = this.mockCourses.find(c => c.id === courseId);
    if (!found) {
      return throwError(() => new Error('Курс не найден')).pipe(delay(300));
    }
    const language = this.getNormalizedLanguage(found.language ?? 'javascript');
    const modules = this.generateModules(courseId, language);
    const courseDetail = { ...found, modules };
    return of(courseDetail).pipe(delay(400));
  }

  private getNormalizedLanguage(lang: string): string {
    const lower = lang.toLowerCase();
    if (lower.includes('angular')) return 'angular';
    if (lower.includes('python')) return 'python';
    if (lower.includes('c#')) return 'csharp';
    return 'javascript';
  }

  private generateModules(courseId: string, language: string) {
    const baseModules = [
      { title: 'Введение', duration: 2, tasksCount: 3 },
      { title: 'Основная часть', duration: 5, tasksCount: 6 },
      { title: 'Продвинутый уровень', duration: 4, tasksCount: 4 },
      { title: 'Практический проект', duration: 6, tasksCount: 5 }
    ];
    return baseModules.map((m, i) => {
      const moduleId = `${courseId}_${i}`;
      const isFirstModule = (i === 0);
      return {
        id: moduleId,
        title: m.title,
        duration: m.duration,
        tasksCount: m.tasksCount,
        topics: [
          {
            id: `${moduleId}_0`,
            title: isFirstModule ? `Основы ${this.getLanguageDisplayName(language)}` : `Тема ${i * 2 + 1}`,
            description: this.getTopicDescription(language, i, 0),
            tasks: this.generateTasksForTopic(`${moduleId}_0`, language, true)
          },
          {
            id: `${moduleId}_1`,
            title: `Тема ${i * 2 + 2}`,
            description: this.getTopicDescription(language, i, 1),
            tasks: this.generateTasksForTopic(`${moduleId}_1`, language, false)
          }
        ]
      };
    });
  }

  private getLanguageDisplayName(language: string): string {
    switch (language) {
      case 'angular': return 'Angular';
      case 'python': return 'Python';
      case 'csharp': return 'C#';
      default: return 'Программирования';
    }
  }

  private getTopicDescription(language: string, moduleIndex: number, topicIndex: number): string {
    const langName = this.getLanguageDisplayName(language);
    if (moduleIndex === 0 && topicIndex === 0) {
      switch (language) {
        case 'angular':
          return `<div>Angular — это платформа для создания клиентских приложений. В этой теме вы изучите <strong>сигналы</strong> и компоненты.</div>
                <pre><code>const count = signal(0);
count.set(5);
console.log(count()); // 5</code></pre>`;
        case 'python':
          return `<div>Python — интерпретируемый язык с простым синтаксисом. Вы научитесь писать функции и работать с данными.</div>
                <pre><code>def greet(name):
    return f"Привет, {name}!"</code></pre>`;
        case 'csharp':
          return `<div>C# — современный объектно-ориентированный язык. Первая программа:</div>
                <pre><code>using System;
class Program {
    static void Main() {
        Console.WriteLine("Hello, World!");
    }
}</code></pre>`;
        default:
          return `<div>Основы ${langName}. Изучите синтаксис и напишите первый код.</div>`;
      }
    }
    // Для остальных тем – краткое описание
    return `<div>Продолжайте изучение ${langName}. В этой теме вас ждут практические упражнения.</div>`;
  }

  private generateTasksForTopic(topicId: string, language: string, isFirstTopic: boolean): ITask[] {
    if (!isFirstTopic) {
      return [
        { id: `${topicId}_task1`, title: 'Задание 1', points: 10, type: 'theory', taskDescription: 'Прочитайте материал модуля.' },
        { id: `${topicId}_task2`, title: 'Задание 2', points: 25, type: 'theory', taskDescription: 'Выполните упражнения.' },
        { id: `${topicId}_task3`, title: 'Задание 3', points: 50, type: 'theory', taskDescription: 'Проверьте знания.' }
      ];
    }

    switch (language) {
      case 'angular':
        return [
          { id: `${topicId}_task1`, title: 'Задание 1', points: 10, type: 'theory', taskDescription: 'Изучите сигналы.' },
          { id: `${topicId}_task2`, title: 'Задание 2', points: 25, type: 'code',
            codeStarter: 'import { Component, signal } from "@angular/core";\n\n@Component({\n  selector: "app-counter",\n  template: `<button (click)="increment()">Click me</button>`\n})\nexport class CounterComponent {\n  count = signal(0);\n  increment() {\n    // увеличьте count на 1\n  }\n}',
            codeSolution: 'import { Component, signal } from "@angular/core";\n\n@Component({\n  selector: "app-counter",\n  template: `<button (click)="increment()">Click me</button>`\n})\nexport class CounterComponent {\n  count = signal(0);\n  increment() {\n    this.count.set(this.count() + 1);\n  }\n}',
            codeLanguage: 'typescript',
            taskDescription: 'Допишите метод increment.'
          },
          { id: `${topicId}_task3`, title: 'Задание 3', points: 50, type: 'quiz', taskDescription: 'Ответьте на вопросы.' }
        ];
      case 'python':
        return [
          { id: `${topicId}_task1`, title: 'Задание 1', points: 10, type: 'theory', taskDescription: 'Изучите функции.' },
          { id: `${topicId}_task2`, title: 'Задание 2', points: 25, type: 'code',
            codeStarter: 'def sum(a, b):\n    # верните сумму',
            codeSolution: 'def sum(a, b):\n    return a + b',
            codeLanguage: 'python',
            taskDescription: 'Реализуйте функцию sum.'
          },
          { id: `${topicId}_task3`, title: 'Задание 3', points: 50, type: 'quiz', taskDescription: 'Ответьте.' }
        ];
      case 'csharp':
        return [
          { id: `${topicId}_task1`, title: 'Задание 1', points: 10, type: 'theory', taskDescription: 'Изучите методы.' },
          { id: `${topicId}_task2`, title: 'Задание 2', points: 25, type: 'code',
            codeStarter: 'public static int Sum(int a, int b) {\n    // верните сумму\n}',
            codeSolution: 'public static int Sum(int a, int b) {\n    return a + b;\n}',
            codeLanguage: 'csharp',
            taskDescription: 'Реализуйте метод Sum.'
          },
          { id: `${topicId}_task3`, title: 'Задание 3', points: 50, type: 'quiz', taskDescription: 'Ответьте.' }
        ];
      default:
        return [
          { id: `${topicId}_task1`, title: 'Задание 1', points: 10, type: 'theory' },
          { id: `${topicId}_task2`, title: 'Задание 2', points: 25, type: 'code',
            codeStarter: '// код',
            codeSolution: '// решение',
            codeLanguage: 'javascript'
          },
          { id: `${topicId}_task3`, title: 'Задание 3', points: 50, type: 'quiz' }
        ];
    }
  }
}
