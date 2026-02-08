import {HttpInterceptorFn} from '@angular/common/http';
import {inject} from '@angular/core';
import {environment} from '../../../environments/environment';
import {IdentityStore} from '../store/identity.store';

export const identityInterceptor: HttpInterceptorFn = (req, next) => {
  const identityStore = inject(IdentityStore);

  if (!req.url.startsWith(environment.apiUrl)) return next(req);
  if (environment.publicUrlRoots.some((publicUrl) => req.url.includes(publicUrl))) return next(req);

  const token = identityStore.accessToken;
  if (!token()) return next(req);

  return next(
    req.clone({
      setHeaders: { Authorization: `Bearer ${token()}` },
    })
  );
};
