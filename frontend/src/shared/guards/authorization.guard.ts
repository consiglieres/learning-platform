import {inject} from '@angular/core';
import {CanActivateFn, Router} from '@angular/router';
import {UserService} from '../../entities/user.service';
//
// export const authorizationGuard: CanActivateFn = async () => {
//   const userService = inject(UserService);
//   const router = inject(Router);
//
//   const cachedUser = userService._userDataSubject.value; // (1)
//
//   if (cachedUser) {
//     return true;
//   }
//
//   try {
//     await userService.getUserDataPromise();
//     return true;
//   } catch {
//     return router.parseUrl('codevia');
//   }
// };

export const authorizationGuard: CanActivateFn = async () => {
  const userService = inject(UserService);
  const router = inject(Router);

  if (userService.currentUser) {
    return true;
  }

  try {
    await userService.getUserDataPromise();
    return true;
  } catch {
    return router.parseUrl('codevia');
  }
};
