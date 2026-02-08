import {computed} from '@angular/core';
import {patchState, signalStore, withComputed, withMethods, withState} from '@ngrx/signals';
import {User} from '../api/models/user';

export interface UserState {
  user: User | null | undefined,
  isLoading: boolean,
}

const initialState: UserState = {
  user: undefined,
  isLoading: false,
}

export const UserStore = signalStore(
  {providedIn: 'root'},
  withState(initialState),
  withComputed((store) => ({
    isLogged: computed<boolean>(() => store.user !== null && store.user !== undefined),
  })),
  withMethods((
    store,
  ) => ({
    setUser(user: User): void {
      patchState(store, (state) => ({...state, user: user}))
    },
    clearUser(): void {
      patchState(store, (state) => ({...state, user: null}))
    }
  }))
)
