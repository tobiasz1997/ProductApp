import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {catchError, Observable, throwError} from 'rxjs';
import {LoggerService} from '../../../shared/services/logger.service';
import {removeOuterQuotes} from '../../../shared/utils/string';
import {environment} from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FavouritesApiService {
  private readonly _apiUrl = environment.apiUrl;
  private readonly _apiPath = 'favourites';

  private readonly _httpClient = inject(HttpClient);
  private readonly _loggerService = inject(LoggerService);

  getFavourites(): Observable<string[]> {
    return this._httpClient
      .get<string[]>(`${this._apiUrl}/${this._apiPath}`)
  };

  addFavourite(productId: string): Observable<void> {
    return this._httpClient
      .put<void>(`${this._apiUrl}/${this._apiPath}/${productId}`, null)
      .pipe(
        catchError((err: HttpErrorResponse) => {
          if (err.error) {
            this._loggerService.logError(removeOuterQuotes(err.error));
          }
          throw throwError(() => err);
        }),
      )
  };

  deleteFavourite(productId: string): Observable<void> {
    return this._httpClient
      .delete<void>(`${this._apiUrl}/${this._apiPath}/${productId}`)
      .pipe(
        catchError((err: HttpErrorResponse) => {
          if (err.error) {
            this._loggerService.logError(removeOuterQuotes(err.error));
          }
          throw throwError(() => err);
        }),
      )
  };
}
