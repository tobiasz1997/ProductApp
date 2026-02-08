import {inject, Injectable} from '@angular/core';
import {DialogService, DynamicDialogRef} from 'primeng/dynamicdialog';
import {LoginDialog} from './login-dialog';
import {defaultDialogConfig} from '../../../shared/const/dialog-config';

@Injectable(
  {providedIn: 'root'}
)
export class LoginDialogService {
  private _dialogService = inject(DialogService)

  open(): DynamicDialogRef<LoginDialog> | null {
    return this._dialogService.open(LoginDialog, defaultDialogConfig('Login'))
  }
}
