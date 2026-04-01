import {inject} from '@angular/core';
import {CanActivateFn, Router} from '@angular/router';
import {UserService} from '../../entities/user.service';

export const authorizationGuard: CanActivateFn = async () => {
  const userService = inject(UserService);
  const router = inject(Router);

  const cachedUser = userService._userDataSubject.value;

  if (cachedUser) {
    return true;
  }

  try {
    await userService.getUserDataPromise();
    return true;
  } catch {
    return router.parseUrl('codevia');
  }
};
