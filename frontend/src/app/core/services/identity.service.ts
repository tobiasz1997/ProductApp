import {inject, Injectable} from '@angular/core';
import {BehaviorSubject, catchError, forkJoin, ignoreElements, map, Observable, of, switchMap, tap} from 'rxjs';
import {UserApiService} from '../api/services/user-api.service';
import {User} from '../api/models/user';
import {LoggerService} from '../../shared/services/logger.service';
import {FavouritesService} from './favourites.service';
import {LoginRequest} from '../api/models/login-request';

@Injectable({
  providedIn: 'root',
})
export class IdentityService {
  private readonly _accessToken$ = new BehaviorSubject<string | null>(null);
  private readonly _user$ = new BehaviorSubject<User | null>(null);

  get user$(): Observable<User | null> {
    return this._user$.asObservable();
  }

  get accessToken(): string | null {
    return this._accessToken$.value;
  }

  private readonly _loggerService = inject(LoggerService);
  private readonly _userApiService = inject(UserApiService);
  private readonly _favouritesService = inject(FavouritesService);

  loginOrCreate(payload: LoginRequest): Observable<boolean> {
    return this._userApiService
      .loginOrCreate(payload)
      .pipe(
        tap((res) => this.setAccessToken(res)),
        switchMap(() => this.loadData()),
        map(() => true as const),
        catchError((_: unknown) => {
          return of(false);
        })
      )
  }

  loadData(): Observable<void> {
    return forkJoin([
      this.getUser(),
      this._favouritesService.getFavourites()
    ]).pipe(
      map(() => void 0)
    )
  }

  getUser(): Observable<User> {
    return this._userApiService
      .getUser()
      .pipe(
        tap(user => this.setUser(user))
      )
  }

  logout(): Observable<boolean> {
    return this._userApiService.logout().pipe(
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
