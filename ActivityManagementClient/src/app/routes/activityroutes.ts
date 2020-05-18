import { Routes } from '@angular/router';

export const ActivityRoutes: Routes = [
  {
    path: '', redirectTo: '/auth/login', pathMatch: 'full'
  },
  {
    path: 'auth', redirectTo: '/auth/login', pathMatch: 'full'
  },
  {
    path: 'auth/login', redirectTo: '/auth/login', pathMatch: 'full'
  },
  {
    path: 'auth/register', redirectTo: '/auth/register', pathMatch: 'full'
  },
  { path: 'panel', redirectTo: '/panel', pathMatch: 'full' },
];


