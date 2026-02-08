import {Product} from '../api/models/product';
import {inject} from '@angular/core';
import {patchState, signalStore, withMethods, withState} from '@ngrx/signals';
import {FavouritesApiService} from '../api/services/favourites-api.service';
import {LoggerService} from '../../shared/services/logger.service';
import {rxMethod} from '@ngrx/signals/rxjs-interop';
import {catchError, EMPTY, finalize, map, pipe, switchMap, tap} from 'rxjs';

export interface FavouritesState {
  favourites: Product[],
  isGetLoading: boolean,
  isActionLoading: boolean,
}

const initialState: FavouritesState = {
  favourites: [],
  isGetLoading: false,
  isActionLoading: false,
}

export const FavouritesStore = signalStore(
  {providedIn: 'root'},
  withState(initialState),
  withMethods((
    store,
    favouritesApiService = inject(FavouritesApiService),
    loggerService = inject(LoggerService)
  ) => ({
    getFavourites: rxMethod<void>(
      pipe(
        tap(() => patchState(store, (state) => ({...state, isGetLoading: true}))),
        switchMap(() => favouritesApiService
          .getFavourites()
          .pipe(
            tap(favourites => {
              patchState(store, (state) => ({
                ...state,
                favourites: favourites
              }))
            }),
            finalize(() => patchState(store, (state) => ({...state, isGetLoading: false})))
          ),
        )
      )
    ),
    addFavourite: rxMethod<Product>(
      pipe(
        tap(() => patchState(store, (state) => ({...state, isActionLoading: true}))),
        switchMap((product) => favouritesApiService
          .addFavourite(product)
          .pipe(
            map((id) => ({ product, id })),
            tap(({ product, id}) => patchState(store, (state) => ({
              ...state,
              favourites: [...state.favourites, {...product, id}]
            }))),
            tap(_ => loggerService.logSuccess('Successfully added to favourite.')),
            catchError((_: unknown) => {
              loggerService.logError('Failed to add to favourite.');
              return EMPTY
            }),
            finalize(() => patchState(store, (state) => ({...state, isActionLoading: false})))
          ))
      )
    ),
    deleteFavourite: rxMethod<string>(
      pipe(
        tap(() => patchState(store, (state) => ({...state, isActionLoading: true}))),
        switchMap((productId) => favouritesApiService
          .deleteFavourite(productId)
          .pipe(
            map((_) => ({ productId })),
            tap(({ productId }) => patchState(store, (state) => ({
              ...state,
              favourites: state.favourites.filter((x) => x.id !== productId)
            }))),
            tap(_ => loggerService.logSuccess('Successfully deleted to favourite.')),
            catchError((_: unknown) => {
              loggerService.logError('Failed to remove favourite.');
              return EMPTY
            }),
            finalize(() => patchState(store, (state) => ({...state, isActionLoading: false})))
          )
        )
      )
    ),
    setFavourites(favourites: Product[]): void {
      patchState(store, (state) => ({...state, favourites: favourites}))
    },
  }))
)
