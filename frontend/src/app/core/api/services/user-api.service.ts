import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {catchError, Observable, throwError} from 'rxjs';
import {User} from '../models/user';
import {environment} from '../../../../environments/environment';
import {HttpErrorResponse} from '@angular/common/http';
import {removeOuterQuotes} from '../../../shared/utils/string';
import {LoggerService} from '../../../shared/services/logger.service';

@Injectable(
  {providedIn: 'root'}
)
export class UserApiService {
  private readonly _apiUrl = environment.apiUrl;
  private readonly _apiPath = 'user';
  private readonly _httpClient = inject(HttpClient);
  private readonly _loggerService = inject(LoggerService);

  getUser(): Observable<User> {
    return this._httpClient
      .get<User>(`${this._apiUrl}/${this._apiPath}/me`, { withCredentials: true })
      .pipe(
        catchError((err: HttpErrorResponse) => {
          if (err.error) {
            this._loggerService.logError(removeOuterQuotes(err.error));
          }
          throw throwError(() => err);
        }),
      )
  }
}
