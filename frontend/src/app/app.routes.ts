import { Routes } from '@angular/router';
import {authGuard} from './core/guards/auth.guard';
import {AppRoutes} from './shared/const/app-routes';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./shared/layouts/base-layout/base-layout').then(m => m.BaseLayout),
    children: [
      {
        path: AppRoutes.Home,
        pathMatch: 'full',
        loadComponent: () => import('./module/home/home').then(m => m.Home)
      },
      {
        path: AppRoutes.FavouriteProducts,
        canActivate: [authGuard],
        loadComponent: () => import('./module/favourite-products/favourite-products').then(m => m.FavouriteProducts)
      },
    ]
  },
  { path: '**', redirectTo: '' },
];
