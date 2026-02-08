import {patchState, signalStore, withMethods, withState} from '@ngrx/signals';

export interface IdentityState {
  accessToken: string | null,
}

const initialState: IdentityState = {
  accessToken: null,
}

export const IdentityStore = signalStore(
  {providedIn: 'root'},
  withState(initialState),
  withMethods((
    store,
  ) => ({
    setAccessToken(accessToken: string): void {
      patchState(store, (state) => ({...state, accessToken: accessToken}))
    },
    clearAccessToken(): void {
      patchState(store, (state) => ({...state, accessToken: null}))
    }
  }))
)
