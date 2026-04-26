import { Component } from '@angular/core';
import {CourseService} from '../../entities/course.service';
import {IUserData} from '../../interfaces/user.interface';
import {UserService} from '../../entities/user.service';
import {Observable} from 'rxjs';
import {AsyncPipe} from '@angular/common';
import {Breadcrumbs} from '../../widgets/breadcrumbs/breadcrumbs';


@Component({
  selector: 'app-profile',
  imports: [AsyncPipe, Breadcrumbs],
  templateUrl: './profile.html',
  styleUrl: './profile.scss',
  providers: [CourseService]
})

export class Profile {
  public userData$?: Observable<IUserData | null>;

  constructor(private _userService: UserService) {
      this.userData$ = _userService.userData$
  }
}
