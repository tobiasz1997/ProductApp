import {inject, Injectable} from '@angular/core';
import {BehaviorSubject, catchError, map, Observable, of, tap, throwError} from 'rxjs';
import {LoggerService} from '../../shared/services/logger.service';
import {FavouritesApiService} from '../api/services/favourites-api.service';
import {Product} from '../api/models/product';

@Injectable({
  providedIn: 'root',
})
export class FavouritesService {
  private readonly _favourites$ = new BehaviorSubject<Product[]>([]);

  get favourites$(): Observable<Product[]> {
    return this._favourites$.asObservable();
  }

  private readonly _favouritesApiService = inject(FavouritesApiService);
  private readonly _loggerService = inject(LoggerService);

  getFavourites(): Observable<boolean> {
    return this._favouritesApiService
      .getFavourites()
      .pipe(
        tap(favourites => this.setFavourites(favourites)),
        map(() => true as const),
        catchError((_: unknown) => {
          return of(true as const);
        }))
  }

  addFavourite(product: Product): Observable<string> {
    return this._favouritesApiService
      .addFavourite(product)
      .pipe(
        tap((id) => this.updateFavouritesAddFavourite(id, product)),
        tap(_ => this._loggerService.logSuccess('Successfully added to favourite.')),
        catchError((error: unknown) => {
          this._loggerService.logError('Failed to add to favourite.');
          return throwError(() => error)
        })
      )
  }

  deleteFavourite(productId: string): Observable<void> {
    return this._favouritesApiService
      .deleteFavourite(productId)
      .pipe(
        tap(_ => this.updateFavouritesDeleteFavourite(productId)),
        tap(_ => this._loggerService.logSuccess('Successfully deleted to favourite.')),
        catchError((error: unknown) => {
          this._loggerService.logError('Failed to remove favourite.');
          return throwError(() => error)
        })
      )
  }

  private updateFavouritesAddFavourite(id: string, product: Product): void {
    this._favourites$.next([...this._favourites$.value, {...product, id}])
  }

  private updateFavouritesDeleteFavourite(productId: string): void {
    this._favourites$.next([...this._favourites$.value.filter((x) => x.id !== productId)])
  }

  private setFavourites(value: Product[]): void {
    this._favourites$.next(value);
  }
}
