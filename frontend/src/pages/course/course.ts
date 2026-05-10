// course.component.ts
import { Component, inject, signal, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { RouterLink } from '@angular/router';
import { CourseService } from '../../entities/course.service';
import { ICourseCategory } from '../../interfaces/courses.interface';

@Component({
  selector: 'app-course',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './course.html',
  styleUrls: ['./course.scss']
})
export class Course implements OnInit {
  private courseService = inject(CourseService);

  public categories = signal<ICourseCategory[]>([]);
  public loading = signal(true);
  public error = signal<string | null>(null);

  // Статические блоки (оставляем как есть)
  public breadcrumbs = signal(['Главная', 'Курс']);
  public courseTitle = signal('Fullstack JavaScript-разработчик');
  public languages = signal(['JavaScript', 'TypeScript', 'Node.js']);
  public headerDescription = signal(
    'Освойте востребованный стек технологий на практике. Проекты, максимально приближённые к реальным задачам индустрии, и экосистема поддержки, которая не даст сойти с дистанции.'
  );

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

  ngOnInit() {
    this.loadCategories();
  }

  private loadCategories() {
    this.loading.set(true);
    this.courseService.getCategories()
      .pipe(takeUntilDestroyed())
      .subscribe({
        next: (data: ICourseCategory[]) => {
          this.categories.set(data);
          this.loading.set(false);
        },
        error: (err) => {
          this.error.set('Ошибка загрузки категорий');
          this.loading.set(false);
          console.error(err);
        }
      });
  }
}
