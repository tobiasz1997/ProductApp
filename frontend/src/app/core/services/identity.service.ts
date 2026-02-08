import {inject, Injectable} from '@angular/core';
import {catchError, map, Observable, of, switchMap, tap, throwError} from 'rxjs';
import {LoggerService} from '../../shared/services/logger.service';
import {SignUpRequest} from '../api/models/sign-up-request';
import {IdentityApiService} from '../api/services/identity-api.service';
import {FavouritesStore} from '../store/favourites.store';
import {UserStore} from '../store/user.store';
import {UserApiService} from '../api/services/user-api.service';
import {User} from '../api/models/user';
import {IdentityStore} from '../store/identity.store';

@Injectable({
  providedIn: 'root',
})
export class IdentityService {
  private readonly _loggerService = inject(LoggerService);
  private readonly _userApiService = inject(UserApiService);
  private readonly _identityApiService = inject(IdentityApiService);
  private readonly _favouriteStore = inject(FavouritesStore);
  private readonly _userStore = inject(UserStore);
  private readonly _identityStore = inject(IdentityStore);


  signIn(payload: SignUpRequest): Observable<boolean> {
    return this._identityApiService
      .signIn(payload)
      .pipe(
        tap((token) => this._identityStore.setAccessToken(token)),
        switchMap(() => this.loadData()),
        map(() => true as const),
        catchError((_: unknown) => {
          return of(false);
        })
      )
  }

  signUp(payload: SignUpRequest): Observable<boolean> {
    return this._identityApiService
      .signUp(payload)
      .pipe(
        tap((token) => this._identityStore.setAccessToken(token)),
        switchMap(() => this.loadData()),
        map(() => true as const),
        catchError((_: unknown) => {
          return of(false);
        })
      )
  }

  logout(): Observable<boolean> {
    return this._identityApiService.logout().pipe(
      switchMap(() => {
        this._identityStore.clearAccessToken();
        this._userStore.clearUser();
        this._loggerService.logSuccess('Successfully logged out.');
        return of(true);
      }),
      catchError(() => of(false))
    );
  }

  loadData(): Observable<boolean> {
    return this.getUser()
      .pipe(
        tap(() => this._favouriteStore.getFavourites()),
        map(() => true as const),
        catchError((_: unknown) => {
          return of(false);
        })
      )
  }

  getUser(): Observable<User> {
    return this._userApiService
      .getUser()
      .pipe(
        tap(user => this._userStore.setUser(user)),
        catchError((error: unknown) => {
          this._userStore.clearUser();
          return throwError(() => error);
        })
      )
  }
}
