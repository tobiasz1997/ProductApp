import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeuix/themes/aura';
import { routes } from './app.routes';
import {DialogService} from 'primeng/dynamicdialog';
import {provideHttpClient, withInterceptors} from '@angular/common/http';
import {MessageService} from 'primeng/api';
import {credentialsInterceptor} from './core/interceptors/credentials.interceptor';
import {identityInterceptor} from './core/interceptors/identity.interceptor';
import {refreshInterceptor} from './core/interceptors/refresh-token.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(
      withInterceptors([credentialsInterceptor, identityInterceptor, refreshInterceptor])
    ),
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    providePrimeNG({
      theme: {
        preset: Aura,
        options: {
          darkModeSelector: 'none'
        }
      }
    }),
    DialogService,
    MessageService
  ]
};
