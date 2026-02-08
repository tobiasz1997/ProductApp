import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import {filter, map, take} from 'rxjs';
import {UserStore} from '../store/user.store';
import {toObservable} from '@angular/core/rxjs-interop';

export const authGuard: CanActivateFn = () => {
  const userStore = inject(UserStore);
  const router = inject(Router);

  return toObservable(userStore.user).pipe(
    filter((user) => user !== undefined),
    take(1),
    map((user) => {
      if (user) {
        return true;
      } else {
        return router.createUrlTree(['/'])
      }
    })
  )
};
