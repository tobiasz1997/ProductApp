import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./module/layouts/base-layout/base-layout').then(m => m.BaseLayout),
    children: [
      {
        path: '',
        pathMatch: 'full',
        loadComponent: () => import('./module/home/home').then(m => m.Home)
      },
    ]
  },
  { path: '**', redirectTo: '' },
];
