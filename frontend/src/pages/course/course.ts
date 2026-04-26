import { Component } from '@angular/core';
import {Router, RouterLink} from '@angular/router';
import {CourseService} from '../../entities/course.service';
import {ITopic} from '../../interfaces/courses.interface';
import {Topic} from '../../features/topic/topic';
import {FilterService} from '../../entities/filter.service';

@Component({
  selector: 'app-course',
  imports: [
    Topic
  ],
  templateUrl: './course.html',
  styleUrl: './course.scss',
  providers: [CourseService, FilterService]
})
export class Course {
  public contents!: ITopic[];

  constructor(private _router: Router, private _courseService: CourseService) {
      _courseService.getCoursesContentTopic().subscribe(result => {
        this.contents = result;
      })
  }

  public descriptions:{title: string, description: string}[] = descriptions;
}

export let descriptions: {title: string, description: string}[] = [
  {
    title: 'Изучите теорию',
    description: 'с подробными уроками и объяснениями',
  },
  {
    title: 'Приступите к практике',
    description: 'где вы можете выбрать заданине по сложоности',
  },
  {
    title: 'Отработайте ошибки',
    description: 'и изучайте курс дальше',
  },
]
