import { Component, inject } from '@angular/core';
import { Observable } from 'rxjs';
import {AsyncPipe, DatePipe} from '@angular/common';
import { UserService } from '../../entities/user.service';
import { CourseService } from '../../entities/course.service';
import { ModalService } from '../../entities/modal.service';
import { IUserData } from '../../interfaces/user.interface';
import { ICourse } from '../../interfaces/courses.interface';
import { Breadcrumbs } from '../../widgets/breadcrumbs/breadcrumbs';

@Component({
  selector: 'app-profile',
  imports: [AsyncPipe, Breadcrumbs, DatePipe],
  templateUrl: './profile.html',
  styleUrls: ['./profile.scss']
})
export class Profile {
  private readonly _userService = inject(UserService);
  private readonly _courseService = inject(CourseService);
  public readonly modalService = inject(ModalService);

  public readonly userData$: Observable<IUserData | null> = this._userService.userData$;
  public readonly courses$: Observable<ICourse[]> = this._courseService.getMyCourses();
}
