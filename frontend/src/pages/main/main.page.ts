import { Component, inject, signal, OnInit } from '@angular/core';
import { CourseService } from '../../entities/course.service';
import { CourseCard } from '../../features/course-card/course-card';
import { FilterService } from '../../entities/filter.service';
import { ICourse } from '../../interfaces/courses.interface';
import { take } from 'rxjs';

@Component({
  selector: 'app-main',
  imports: [CourseCard],
  templateUrl: './main.page.html',
  styleUrls: ['./main.page.scss'],
})
export class MainPage implements OnInit {
  private readonly _courseService = inject(CourseService);
  private readonly _filterService = inject(FilterService);

  private allCourses: ICourse[] = [];

  public displayedCourses = signal<ICourse[]>([]);

  public searchData = signal('');
  public direction = signal('');
  public difficulty = signal('');
  public technology = signal('');

  ngOnInit(): void {
    this._courseService.getCourses().pipe(take(1)).subscribe(courses => {
      this.allCourses = courses;
      this.displayedCourses.set([...courses]); // показываем все
    });
  }

  public sendFiltersData(): void {
    const filters = {
      search: this.searchData(),
      direction: this.direction(),
      difficulty: this.difficulty(),
      technology: this.technology(),
    };
    const filtered = this._filterService.applyFilters(this.allCourses, filters);
    this.displayedCourses.set(filtered);
  }
}
