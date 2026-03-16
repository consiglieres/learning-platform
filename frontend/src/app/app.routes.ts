import { Routes } from '@angular/router';
import { MainLayout } from '../pages/layouts/main-layout/main-layout';
import { MainPage } from '../pages/main.page/main.page';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'learning-platform',
    pathMatch: 'full',
  },
  {
    path: 'learning-platform',
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
    ],
  },
];
