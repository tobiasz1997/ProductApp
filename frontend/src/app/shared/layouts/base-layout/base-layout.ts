import {ChangeDetectionStrategy, Component, DestroyRef, inject} from '@angular/core';
import {RouterLink, RouterLinkActive, RouterOutlet} from '@angular/router';
import {Button, ButtonDirective} from 'primeng/button';
import {LoginDialogService} from '../../../module/auth/login-dialog/login-dialog.service';
import {IdentityService} from '../../../core/services/identity.service';
import {takeUntilDestroyed, toSignal} from '@angular/core/rxjs-interop';
import {User} from '../../../core/api/models/user';
import {favouriteProductsUrl, homeUrl} from '../../const/routes';

@Component({
  selector: 'app-base-layout',
  imports: [
    RouterOutlet,
    Button,
    RouterLink,
    ButtonDirective,
    RouterLinkActive
  ],
  templateUrl: './base-layout.html',
  styleUrl: './base-layout.scss',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BaseLayout {
  private readonly _loginDialogService = inject(LoginDialogService)
  private readonly _identityService = inject(IdentityService)
  private readonly _destroyRef = inject(DestroyRef)

  protected readonly favouriteProductsUrl = favouriteProductsUrl;
  protected readonly homeUrl = homeUrl;

  user = toSignal<User | null | undefined>(this._identityService.user$, {initialValue: null})

  login(): void {
    this._loginDialogService.open();
  }

  logout(): void {
    this._identityService.logout().pipe(takeUntilDestroyed(this._destroyRef)).subscribe();
  }
}
