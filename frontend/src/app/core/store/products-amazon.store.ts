import {Product} from '../api/models/product';
import {inject} from '@angular/core';
import {patchState, signalStore, withMethods, withState} from '@ngrx/signals';
import {rxMethod} from '@ngrx/signals/rxjs-interop';
import {finalize, pipe, switchMap, tap} from 'rxjs';
import {ProductApiService} from '../api/services/product-api.service';

export interface ProductsAmazonState {
  products: Product[],
  isLoading: boolean,
}

const initialState: ProductsAmazonState = {
  products: [],
  isLoading: false,
}

export const ProductsAmazonStore = signalStore(
  {providedIn: 'root'},
  withState(initialState),
  withMethods((
    store,
    productsApiService = inject(ProductApiService),
  ) => ({
    getProducts: rxMethod<void>(
      pipe(
        tap(() => patchState(store, (state) => ({...state, isLoading: true}))),
        switchMap(() => productsApiService
          .getProducts()
          .pipe(
            tap(products => {
              patchState(store, (state) => ({
                ...state,
                products: products
              }))
            }),
            finalize(() => patchState(store, (state) => ({...state, isLoading: false})))
          ),
        )
      )
    ),
  }))
)
