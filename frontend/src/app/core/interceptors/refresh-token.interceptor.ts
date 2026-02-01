import {HttpErrorResponse, HttpInterceptorFn} from '@angular/common/http';
import {inject} from '@angular/core';
import {catchError, switchMap, throwError} from 'rxjs';
import {IdentityService} from '../services/identity.service';
import {UserApiService} from '../api/services/user-api.service';
import {environment} from '../../../environments/environment';

export const refreshInterceptor: HttpInterceptorFn = (req, next) => {
  const identityService = inject(IdentityService);
  const userApiService = inject(UserApiService);

  if (!req.url.startsWith(environment.apiUrl)) return next(req);
  if (!req.url.includes('/user/me') && !req.url.includes('/favourites')) return next(req);
  if (req.url.includes('/user/token/refresh') || req.url.includes('/user/logout')) return next(req);

  return next(req).pipe(
    catchError(err => {
      const error401Code = err instanceof HttpErrorResponse && (err.status === 401 || err.status === 0);
      if (!error401Code) return throwError(() => err);

      return userApiService.refreshToken().pipe(
        switchMap(newToken => {
          identityService.setAccessToken(newToken);
          return next(
            req.clone({
              setHeaders: {Authorization: `Bearer ${newToken}`},
            })
          )
        }),
        catchError(err => {
          return userApiService.logout().pipe(
            switchMap(() => {
              identityService.setAccessToken(null);
              identityService.setUser(null);
              return throwError(() => err);
            })
          );
        })
      );
    })
  );
};
