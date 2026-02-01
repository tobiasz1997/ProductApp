import {inject, Injectable} from '@angular/core';
import {MessageService} from 'primeng/api';
import {environment} from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class LoggerService {
  private readonly isProduction = environment.production
  private readonly _messageService = inject(MessageService);

  public logError(message: string, showToast = true) {
    if (!this.isProduction) {
      console.error(message)
    }
    if (showToast) {
      this._messageService.add({ severity: 'error', summary: 'Error', detail: message });
    }
  }

  public logSuccess(message: string, showToast = true) {
    if (!this.isProduction) {
      console.log(message)
    }
    if (showToast) {
      this._messageService.add({ severity: 'success', summary: 'Success', detail: message });
    }
  }
}
