import {HttpInterceptorFn} from '@angular/common/http';
import {inject} from '@angular/core';
import {IdentityService} from '../services/identity.service';
import {environment} from '../../../environments/environment';

export const identityInterceptor: HttpInterceptorFn = (req, next) => {
  const identityService = inject(IdentityService);

  if (!req.url.startsWith(environment.apiUrl)) return next(req);
  if (environment.publicUrlRoots.some((publicUrl) => req.url.includes(publicUrl))) return next(req);

  const token = identityService.accessToken;
  if (!token) return next(req);

  return next(
    req.clone({
      setHeaders: { Authorization: `Bearer ${token}` },
    })
  );
};
