import { Component, inject, signal, OnInit } from '@angular/core';
import {ActivatedRoute, RouterLink} from '@angular/router';
import { CourseService } from '../../entities/course.service';
import {switchMap, take} from 'rxjs';
import {MockCourseService} from '../../entities/mock-course.service';

interface ITopic {
  title: string;
  description: string;
}

interface IModule {
  id: string;
  title: string;
  duration: number;
  tasksCount: number;
  topics: ITopic[];
  expanded?: boolean; // локальное состояние раскрытия
}

interface ICourseDetail {
  id: string;
  title: string;
  description: string;
  image?: string;
  duration?: number;
  tasks?: number;
  language?: string;
  categories: any[];
  modules: IModule[];
}

@Component({
  selector: 'app-course',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './course.html',
  styleUrls: ['./course.scss'],
  providers: [MockCourseService]
})

export class Course implements OnInit {
  private route = inject(ActivatedRoute);
  private courseService = inject(CourseService);
  private mockCourseService = inject(MockCourseService)

  public courseDetail = signal<ICourseDetail | null>(null);
  public loading = signal(true);
  public error = signal<string | null>(null);

  // Статические преимущества и описания (можно позже вынести в сервис)
  public advantagesTop = signal([
    { img: 'assets/advantages.png', title: '500+ довольных студентов', desc: 'уже закончили курс' },
    { img: 'assets/advantages.png', title: 'Учись самостоятельно', desc: 'в свободное время' },
    { img: 'assets/advantages.png', title: 'Выбирай сам задания', desc: 'по уровню сложности' }
  ]);

  public advantagesBottom = signal([
    { img: 'assets/advantages.png', title: 'Подходит для тех у кого уже есть опыт,', desc: 'а также для новичков' },
    { img: 'assets/advantages.png', title: 'Ответим на все ваши вопросы', desc: 'быстро и качественно' }
  ]);

  public descriptions = signal([
    { title: 'Изучите теорию', description: 'с подробными уроками и объяснениями' },
    { title: 'Приступите к практике', description: 'где вы можете выбрать задание по сложности' },
    { title: 'Отработайте ошибки', description: 'и изучайте курс дальше' }
  ]);

  // Для хлебных крошек
  public breadcrumbs = signal(['Главная', 'Курс']);

  ngOnInit(): void {
    this.route.paramMap.pipe(
      switchMap(params => {
        const id = params.get('courseId');
        if (id) {
          return this.mockCourseService.getCourseById(id);
        }
        return [];
      }),
      take(1)
    ).subscribe({
      next: (data: any) => {
        if (data.modules) {
          data.modules.forEach((m: IModule) => m.expanded = false);
        }
        this.courseDetail.set(data as ICourseDetail);
        this.loading.set(false);
        // Обновим хлебные крошки заголовком курса
        this.breadcrumbs.set(['Главная', data.title]);
      },
      error: (err) => {
        this.error.set('Ошибка загрузки данных курса');
        this.loading.set(false);
      }
    });
  }

  // Метод для переключения аккордеона
  public toggleModule(module: IModule): void {
    module.expanded = !module.expanded;
  }
}
