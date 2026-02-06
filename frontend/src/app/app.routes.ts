import { Routes } from '@angular/router';
import {favouriteProductsUrl, homeUrl} from './shared/const/routes';
import {authGuard} from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./shared/layouts/base-layout/base-layout').then(m => m.BaseLayout),
    children: [
      {
        path: homeUrl,
        pathMatch: 'full',
        loadComponent: () => import('./module/home/home').then(m => m.Home)
      },
      {
        path: favouriteProductsUrl,
        canActivate: [authGuard],
        loadComponent: () => import('./module/favourite-products/favourite-products').then(m => m.FavouriteProducts)
      },
    ]
  },
  { path: '**', redirectTo: '' },
];
