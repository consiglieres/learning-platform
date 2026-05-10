import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { UserService } from '../entities/user.service';
import { MockUserService } from '../entities/mock-user.service';   // путь поправь под свой
import { environment } from '../app/environmets';
import {CourseService} from '../entities/course.service';
import {MockCourseService} from '../entities/mock-course.service';      // путь к environment

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    // Подмена UserService
    {
      provide: UserService,
      useClass: environment.useMocks ? MockUserService : UserService
    },
    {
      provide: CourseService,
      useClass: environment.useMocks ? MockCourseService : CourseService
    }
  ]
};
