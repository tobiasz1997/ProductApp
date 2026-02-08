import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {catchError, map, Observable, throwError} from 'rxjs';
import {LoggerService} from '../../../shared/services/logger.service';
import {HttpErrorResponse} from '@angular/common/http';
import {removeOuterQuotes} from '../../../shared/utils/string';
import {SignUpRequest} from '../models/sign-up-request';
import {environment} from '../../../../environments/environment';
import {SignInRequest} from '../models/sign-in-request';

@Injectable(
  {providedIn: 'root'}
)
export class IdentityApiService {
  private readonly _apiUrl = environment.apiUrl;
  private readonly _apiPath = 'identity';
  private readonly _httpClient = inject(HttpClient);
  private readonly _loggerService = inject(LoggerService);

  signIn(request: SignInRequest): Observable<string> {
    return this._httpClient
      .post(`${this._apiUrl}/${this._apiPath}/sign-in`, request, {responseType: 'text'})
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

  signUp(request: SignUpRequest): Observable<string> {
    return this._httpClient
      .post(`${this._apiUrl}/${this._apiPath}/sign-up`, request, {responseType: 'text'})
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
