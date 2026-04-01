import { Component } from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {HeaderComponent} from '../../../widgets/header.component/header.component';

@Component({
  selector: 'app-course-view',
  imports: [
    RouterOutlet,
    HeaderComponent
  ],
  templateUrl: './course-view.html',
  styleUrl: './course-view.scss',
})
export class CourseView {

}
