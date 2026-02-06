import {inject, Injectable} from '@angular/core';
import {DialogService, DynamicDialogRef} from 'primeng/dynamicdialog';
import {defaultDialogConfig} from '../../../shared/const/dialog-config';
import {RegisterDialog} from './register-dialog';

@Injectable(
  {providedIn: 'root'}
)
export class RegisterDialogService {
  private _dialogService = inject(DialogService)

  open(): DynamicDialogRef<RegisterDialog> | null {
    return this._dialogService.open(RegisterDialog, defaultDialogConfig('Register'));
  }
}
