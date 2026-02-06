import {ChangeDetectionStrategy, Component, inject, model, signal} from '@angular/core';
import {Button} from 'primeng/button';
import {DynamicDialogRef} from 'primeng/dynamicdialog';
import {Tooltip} from 'primeng/tooltip';
import {InputText} from 'primeng/inputtext';
import {ReactiveFormsModule} from '@angular/forms';
import {form, FormField, required, maxLength, minLength} from '@angular/forms/signals';
import {IdentityService} from '../../../core/services/identity.service';
import {SignInRequest} from '../../../core/api/models/sign-in-request';
import {RegisterDialogService} from '../register-dialog/register-dialog.service';
import {FormInput} from '../../../shared/components/form/form-input/form-input';

@Component({
  selector: 'app-login-dialog',
  imports: [
    Button,
    ReactiveFormsModule,
    FormField,
    FormInput
  ],
  templateUrl: './login-dialog.html',
  styleUrl: './login-dialog.scss',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginDialog {
  loginModel = model<SignInRequest>({
    login: '',
    password: '',
  });

  loginForm = form(this.loginModel, (schemaPath) => {
    required(schemaPath.login, { message: 'Login is required.'});
    required(schemaPath.password, { message: 'Password is required.'});
  });

  private readonly _dynamicDialogRef = inject(DynamicDialogRef);
  private readonly _identityService = inject(IdentityService);
  private readonly _registerDialogService = inject(RegisterDialogService)

  openRegisterDialog(): void {
    this.resetForm();
    this._dynamicDialogRef.close();
    this._registerDialogService.open();
  }

  handleSubmit(): void {
    this.loginForm.login().markAsDirty();
    this.loginForm.password().markAsDirty();
    if (this.loginForm().invalid()) {
      return;
    }

    this._identityService.signIn(this.loginModel())
      .subscribe((isSuccess) => {
        if(isSuccess) {
          this._dynamicDialogRef.close();
        }
      })
  }

  private resetForm(): void {
    this.loginForm().reset();
    this.loginModel.set({ login: '', password: '' });
  }
}
