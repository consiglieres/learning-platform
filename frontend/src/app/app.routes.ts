import { Routes } from '@angular/router';
import { MainLayout } from '../pages/layouts/main-layout/main-layout';
import { MainPage } from '../pages/main.page/main.page';
import {CourseView} from '../pages/layouts/course-view/course-view';
import {Course} from '../pages/course/course';
import {Theme} from '../pages/theme/theme';
import {Profile} from '../pages/profile/profile';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'codevia',
    pathMatch: 'full',
  },
  {
    path: 'codevia',
    component: MainLayout,
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'courses-list',
      },
      {
        path: 'courses-list',
        component: MainPage,
      },
      {
        path: 'profile',
        component: Profile
      }
    ],
  },
  {
    path: 'course/:courseId',
    component: CourseView,
    children: [
      {
        path: '',
        component: Course,
      },
      {
        path: 'theme/:themeId',
        component: Theme
      },
    ]
  }
];
