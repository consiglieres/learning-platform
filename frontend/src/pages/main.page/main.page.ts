import {Component, Signal, signal, WritableSignal} from '@angular/core';
import { CourseService } from '../../entities/course.service';
import { CourseCard } from '../../features/course-card/course-card';

@Component({
  selector: 'app-main.page',
  imports: [CourseCard],
  templateUrl: './main.page.html',
  styleUrl: './main.page.scss',
  providers: [CourseService],
})
export class MainPage {
  public searchData: WritableSignal<string> = signal<string>('');
  public direction: WritableSignal<string> = signal<string>('');
  public difficulty: WritableSignal<string> = signal<string>('');
  public technology: WritableSignal<string> = signal<string>('');

  constructor(private _courseService: CourseService) {}

  public sendFiltersData(): void {
    const filtersData = {
      search: this.searchData(),
      direction: this.direction(),
      difficulty: this.difficulty(),
      technology: this.technology(),
    };
    console.log(filtersData);
  }
}
