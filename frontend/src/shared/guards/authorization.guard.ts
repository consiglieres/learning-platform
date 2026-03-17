import {CanActivateFn, Router} from '@angular/router';
import {inject} from '@angular/core';
import {CookieService} from '../../entities/cookie.service';

export const authorizationGuard: CanActivateFn = (route, state) => {
  const cookieService = inject(CookieService)
  const router = inject(Router)

  return cookieService.getCookies().length != 0 ? true : router.parseUrl('/login');
};
