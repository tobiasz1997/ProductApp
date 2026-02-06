import {inject, Injectable} from '@angular/core';
import {BehaviorSubject, catchError, concatMap, map, Observable, of, switchMap, tap, throwError} from 'rxjs';
import {UserApiService} from '../api/services/user-api.service';
import {User} from '../api/models/user';
import {LoggerService} from '../../shared/services/logger.service';
import {FavouritesService} from './favourites.service';
import {SignUpRequest} from '../api/models/sign-up-request';
import {IdentityApiService} from '../api/services/identity-api.service';

@Injectable({
  providedIn: 'root',
})
export class IdentityService {
  private readonly _accessToken$ = new BehaviorSubject<string | null>(null);
  private readonly _user$ = new BehaviorSubject<User | null | undefined>(undefined);

  get user$(): Observable<User | null | undefined> {
    return this._user$.asObservable();
  }

  get accessToken(): string | null {
    return this._accessToken$.value;
  }

  private readonly _loggerService = inject(LoggerService);
  private readonly _userApiService = inject(UserApiService);
  private readonly _identityApiService = inject(IdentityApiService);
  private readonly _favouritesService = inject(FavouritesService);

  signIn(payload: SignUpRequest): Observable<boolean> {
    return this._identityApiService
      .signIn(payload)
      .pipe(
        tap((res) => this.setAccessToken(res)),
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
        tap((res) => this.setAccessToken(res)),
        switchMap(() => this.loadData()),
        map(() => true as const),
        catchError((_: unknown) => {
          return of(false);
        })
      )
  }

  loadData(): Observable<boolean> {
    return this.getUser()
      .pipe(
        concatMap(() => this._favouritesService.getFavourites()
          .pipe(map(() => true as const),
            catchError((_: unknown) => {
              return of(true as const);
            }))),
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
        tap(user => this.setUser(user)),
        catchError((error: unknown) => {
          this.setUser(null);
          return throwError(() => error);
        })
      )
  }

  logout(): Observable<boolean> {
    return this._identityApiService.logout().pipe(
      switchMap(() => {
        this.setAccessToken(null);
        this.setUser(null);
        this._loggerService.logSuccess('Successfully logged out.');
        return of(true);
      }),
      catchError(() => of(false))
    );
  }

  setAccessToken(value: string | null): void {
    this._accessToken$.next(value);
  }

  setUser(value: User | null): void {
    this._user$.next(value)
  }
}
