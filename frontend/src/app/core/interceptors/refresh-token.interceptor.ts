import {HttpErrorResponse, HttpInterceptorFn} from '@angular/common/http';
import {inject} from '@angular/core';
import {catchError, switchMap, throwError} from 'rxjs';
import {environment} from '../../../environments/environment';
import {IdentityApiService} from '../api/services/identity-api.service';
import {UserStore} from '../store/user.store';
import {IdentityStore} from '../store/identity.store';

export const refreshInterceptor: HttpInterceptorFn = (req, next) => {
  const userStore = inject(UserStore);
  const identityStore = inject(IdentityStore);
  const identityApiService = inject(IdentityApiService);

  if (!req.url.startsWith(environment.apiUrl)) return next(req);
  if (req.url.startsWith(environment.rapidApiUrl)) return next(req);
  if (environment.publicUrlRoots.some((publicUrl) => req.url.startsWith(publicUrl))) return next(req);

  return next(req).pipe(
    catchError(err => {
      const error401Code = err instanceof HttpErrorResponse && (err.status === 401 || err.status === 0);
      if (!error401Code) return throwError(() => err);

      return identityApiService.refreshToken().pipe(
        switchMap(newToken => {
          identityStore.setAccessToken(newToken);
          return next(
            req.clone({
              setHeaders: {Authorization: `Bearer ${newToken}`},
            })
          )
        }),
        catchError(err => {
          return identityApiService.logout().pipe(
            switchMap(() => {
              identityStore.clearAccessToken();
              userStore.clearUser();
              return throwError(() => err);
            })
          );
        })
      );
    })
  );
};
