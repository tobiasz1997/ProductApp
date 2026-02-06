import {Component, inject, signal} from '@angular/core';
import {SignUpRequest} from '../../../core/api/models/sign-up-request';
import {form, FormField, maxLength, minLength, pattern, required} from '@angular/forms/signals';
import {DynamicDialogRef} from 'primeng/dynamicdialog';
import {IdentityService} from '../../../core/services/identity.service';
import {InputText} from 'primeng/inputtext';
import {Button} from 'primeng/button';
import {LoginDialogService} from '../login-dialog/login-dialog.service';
import {FormInput} from '../../../shared/components/form/form-input/form-input';
import {digitRegex, lowercaseLetterRegex, polishRegex, uppercaseLetterRegex} from '../../../shared/const/patterns';

@Component({
  selector: 'app-register-dialog',
  imports: [
    FormField,
    Button,
    FormInput
  ],
  templateUrl: './register-dialog.html',
  styleUrl: './register-dialog.scss',
  standalone: true
})
export class RegisterDialog {
  loginModel = signal<SignUpRequest>({
    login: '',
    password: '',
  });

  loginForm = form(this.loginModel, (schemaPath) => {
    required(schemaPath.login, { message: 'Login is required.'});
    minLength(schemaPath.login, 5, { message: 'Login must have at least 5 characters' });
    maxLength(schemaPath.login, 50, { message: 'Login is too long - max 50 characters' });
    pattern(schemaPath.login, lowercaseLetterRegex, { message: 'Login must have at least one lowercase letter' });
    pattern(schemaPath.login, uppercaseLetterRegex, { message: 'Login must have at least one uppercase letter' });
    pattern(schemaPath.login, digitRegex, { message: 'Login must have at least one digit' });
    pattern(schemaPath.login, polishRegex, { message: 'Login cannot contain polish letters' });
    required(schemaPath.password, { message: 'Password is required.'});
    minLength(schemaPath.password, 12, { message: 'Password must have at least 2 characters' });
    maxLength(schemaPath.password, 64, { message: 'Password is too long - max 64 characters' });
  });

  private readonly _dynamicDialogRef = inject(DynamicDialogRef);
  private readonly _identityService = inject(IdentityService);
  private readonly _loginDialogService = inject(LoginDialogService)

  openLoginDialog(): void {
    this.resetForm();
    this._dynamicDialogRef.close();
    this._loginDialogService.open();
  }

  handleSubmit(): void {
    this.loginForm.login().markAsDirty();
    this.loginForm.password().markAsDirty();
    if (this.loginForm().invalid()) {
      return;
    }

    this._identityService.signUp(this.loginModel())
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
