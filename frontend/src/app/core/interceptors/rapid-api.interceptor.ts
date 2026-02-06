import {HttpInterceptorFn} from '@angular/common/http';
import {environment} from '../../../environments/environment';

export const rapidApiInterceptor: HttpInterceptorFn = (req, next) => {
  if (!req.url.startsWith(environment.rapidApiUrl)) return next(req);

  return next(
    req.clone({
      setHeaders: {
        'x-rapidapi-key': `${environment.rapidKey}`,
        'x-rapidapi-host': `${environment.rapidHost}`
      },
    })
  );
};
