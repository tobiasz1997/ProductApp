import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import {IdentityService} from '../services/identity.service';
import {filter, map, take} from 'rxjs';
import {homeUrl} from '../../shared/const/routes';

export const authGuard: CanActivateFn = () => {
  const identityService = inject(IdentityService);
  const router = inject(Router);

  return identityService.user$.pipe(
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
