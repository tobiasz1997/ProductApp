import {HttpErrorResponse, HttpInterceptorFn} from '@angular/common/http';
import {inject} from '@angular/core';
import {catchError, switchMap, throwError} from 'rxjs';
import {IdentityService} from '../services/identity.service';
import {environment} from '../../../environments/environment';
import {IdentityApiService} from '../api/services/identity-api.service';

export const refreshInterceptor: HttpInterceptorFn = (req, next) => {
  const identityService = inject(IdentityService);
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
          identityService.setAccessToken(newToken);
          return next(
            req.clone({
              setHeaders: {Authorization: `Bearer ${newToken}`},
            })
          )
        }),
        catchError(err => {
          return identityApiService.logout().pipe(
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
