import {ChangeDetectionStrategy, Component, inject, signal} from '@angular/core';
import {Button} from 'primeng/button';
import {DynamicDialogRef} from 'primeng/dynamicdialog';
import {Tooltip} from 'primeng/tooltip';
import {InputText} from 'primeng/inputtext';
import {ReactiveFormsModule} from '@angular/forms';
import {form, FormField, required, maxLength, minLength} from '@angular/forms/signals';
import {IdentityService} from '../../../core/services/identity.service';
import {LoginRequest} from '../../../core/api/models/login-request';

@Component({
  selector: 'app-login-dialog',
  imports: [
    Button,
    Tooltip,
    InputText,
    ReactiveFormsModule,
    FormField
  ],
  templateUrl: './login-dialog.html',
  styleUrl: './login-dialog.scss',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginDialog {
  loginModel = signal<LoginRequest>({
    login: '',
    password: '',
  });

  loginForm = form(this.loginModel, (schemaPath) => {
    required(schemaPath.login, { message: 'Login is required.'});
    minLength(schemaPath.login, 2, { message: 'Login must have at least 2 characters' });
    maxLength(schemaPath.login, 100, { message: 'Login is too long' });
    required(schemaPath.password, { message: 'Password is required.'});
    minLength(schemaPath.password, 2, { message: 'Password must have at least 2 characters' });
    maxLength(schemaPath.password, 100, { message: 'Password is too long' });
  });

  private readonly _dynamicDialogRef = inject(DynamicDialogRef);
  private readonly _identityService = inject(IdentityService);

  closeDialog(): void {
    this.resetForm();
    this._dynamicDialogRef.close()
  }

  handleSubmit(): void {
    if (this.loginForm().invalid()) {
      return;
    }

    const formData = this.loginModel();

    this._identityService.loginOrCreate(formData)
      .subscribe((isSuccess) => {
        if(isSuccess) {
          this.closeDialog()
        }
      })
  }

  private resetForm(): void {
    this.loginForm().reset();
    this.loginModel.set({ login: '', password: '' });
  }
}
