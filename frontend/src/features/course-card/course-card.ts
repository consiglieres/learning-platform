import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ICourse } from '../../interfaces/courses.interface';

@Component({
  selector: 'app-course-card',
  imports: [RouterLink],
  templateUrl: './course-card.html',
  styleUrls: ['./course-card.scss'],
})
export class CourseCard {
  @Input({ required: true }) course!: ICourse;
}
