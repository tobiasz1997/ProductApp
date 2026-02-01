import {ChangeDetectionStrategy, Component, inject, OnInit} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {Button} from 'primeng/button';
import {DialogService} from 'primeng/dynamicdialog';
import {LoginDialog} from '../../auth/login-dialog/login-dialog';
import {IdentityService} from '../../../core/services/identity.service';
import {toSignal} from '@angular/core/rxjs-interop';
import {User} from '../../../core/api/models/user';

@Component({
  selector: 'app-base-layout',
  imports: [
    RouterOutlet,
    Button
  ],
  templateUrl: './base-layout.html',
  styleUrl: './base-layout.scss',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BaseLayout implements OnInit {
  private _dialogService = inject(DialogService)
  private _identityService = inject(IdentityService)

  user = toSignal<User | null>(this._identityService.user$, { initialValue: null })

  ngOnInit() {
    this._identityService.loadData().subscribe();
  }

  login(): void {
    this._dialogService.open(LoginDialog, {
      header: 'Login',
      modal: true,
      width: '50vw',
      closable: true
    })
  }

  logout(): void {
    this._identityService.logout().subscribe();
  }
}
