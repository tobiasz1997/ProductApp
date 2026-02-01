import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {catchError, map, Observable, throwError} from 'rxjs';
import {User} from '../models/user';
import {LoggerService} from '../../../shared/services/logger.service';
import {HttpErrorResponse} from '@angular/common/http';
import {removeOuterQuotes} from '../../../shared/utils/string';
import {LoginRequest} from '../models/login-request';
import {environment} from '../../../../environments/environment';

@Injectable(
  {providedIn: 'root'}
)
export class UserApiService {
  private readonly _apiUrl = environment.apiUrl;
  private readonly _apiPath = 'user';
  private readonly _httpClient = inject(HttpClient);
  private readonly _loggerService = inject(LoggerService);

  loginOrCreate(request: LoginRequest): Observable<string> {
    return this._httpClient
      .post(`${this._apiUrl}/${this._apiPath}/sign-in-or-create`, request, {responseType: 'text'})
      .pipe(
        map(response => response as string),
        catchError((err: HttpErrorResponse) => {
          if (err.error) {
            this._loggerService.logError(removeOuterQuotes(err.error));
          }
          return throwError(() => err);
        })
      )
  }

  getUser(): Observable<User> {
    return this._httpClient
      .get<User>(`${this._apiUrl}/${this._apiPath}/me`)
  }

  refreshToken(): Observable<string> {
    return this._httpClient
      .post(`${this._apiUrl}/${this._apiPath}/token/refresh`, null, {responseType: 'text'})
      .pipe(
        map(response => response as string),
      )
  }

  logout(): Observable<void> {
    return this._httpClient
      .post<void>(`${this._apiUrl}/${this._apiPath}/logout`, null)
  }
}
