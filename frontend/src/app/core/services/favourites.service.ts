import {inject, Injectable} from '@angular/core';
import {BehaviorSubject, catchError, map, Observable, of, switchMap, tap} from 'rxjs';
import {LoggerService} from '../../shared/services/logger.service';
import {FavouritesApiService} from '../api/services/favourites-api.service';

@Injectable({
  providedIn: 'root',
})
export class FavouritesService {
  private readonly _favourites$ = new BehaviorSubject<Set<string>>(new Set([]));

  get favourites$(): Observable<Set<string>> {
    return this._favourites$.asObservable();
  }

  private readonly _favouritesApiService = inject(FavouritesApiService);
  private readonly _loggerService = inject(LoggerService);

  getFavourites(): Observable<string[]> {
    return this._favouritesApiService
      .getFavourites()
      .pipe(
        tap(favourites => this.setFavourites(favourites))
      )
  }

  addFavourite(productId: number): Observable<void> {
    return this._favouritesApiService
      .addFavourite(productId.toString())
      .pipe(
        tap(_ => this.updateFavouritesAddFavourite(productId)),
        tap(_ => this._loggerService.logSuccess('Successfully added to favourite.'))
      )
  }

  deleteFavourite(productId: number): Observable<void> {
    return this._favouritesApiService
      .deleteFavourite(productId.toString())
      .pipe(
        tap(_ => this.updateFavouritesDeleteFavourite(productId)),
        tap(_ => this._loggerService.logSuccess('Successfully deleted to favourite.'))
      )
  }

  private updateFavouritesAddFavourite(productId: number): void {
    const current = new Set(this._favourites$.value);
    current.add(productId.toString());
    this._favourites$.next(current)
  }

  private updateFavouritesDeleteFavourite(productId: number): void {
    const current = new Set(this._favourites$.value);
    current.delete(productId.toString())
    this._favourites$.next(current)
  }

  private setFavourites(value: string[]): void {
    this._favourites$.next(new Set(value));
  }
}
