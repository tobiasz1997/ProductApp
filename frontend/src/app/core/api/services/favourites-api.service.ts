import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {catchError, map, Observable, throwError} from 'rxjs';
import {LoggerService} from '../../../shared/services/logger.service';
import {removeOuterQuotes} from '../../../shared/utils/string';
import {environment} from '../../../../environments/environment';
import {Product} from '../models/product';
import {ProductRequest} from '../models/product-request';

@Injectable({
  providedIn: 'root'
})
export class FavouritesApiService {
  private readonly _apiUrl = environment.apiUrl;
  private readonly _apiPath = 'favourites';
  private readonly _httpClient = inject(HttpClient);
  private readonly _loggerService = inject(LoggerService);

  getFavourites(): Observable<Product[]> {
    return this._httpClient
      .get<Product[]>(`${this._apiUrl}/${this._apiPath}`)
  };

  addFavourite(product: ProductRequest): Observable<string> {
    return this._httpClient
      .post<string>(`${this._apiUrl}/${this._apiPath}`, product)
      .pipe(
        map(response => response as string),
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
