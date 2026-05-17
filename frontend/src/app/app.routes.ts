import { Routes } from '@angular/router';
import { authorizationGuard } from '../shared/guards/authorization.guard';

// Синхронные импорты (будут включены в бандл лейаута)
import { MainPage } from '../pages/main/main.page';
import { Profile } from '../pages/profile/profile';
import { Course } from '../pages/course/course';
import { Theme } from '../pages/theme/theme';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'codevia',
    pathMatch: 'full',
  },
  {
    path: 'codevia',
    loadComponent: () =>
      import('../pages/layouts/main-layout/main-layout').then(
        (m) => m.MainLayout
      ),
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
        component: Profile,
        canActivate: [authorizationGuard],
      },
    ],
  },
  {
    path: 'course/:courseId',
    loadComponent: () =>
      import('../pages/layouts/course-view/course-view').then(
        (m) => m.CourseView
      ),
    canActivate: [authorizationGuard],
    children: [
      {
        path: '',
        component: Course,
      },
      {
        path: 'theme/:themeId',
        component: Theme,
      },
    ],
  },
];
