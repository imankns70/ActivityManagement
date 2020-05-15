import { Routes } from '@angular/router';

export const ActivityRoutes: Routes = [
  {path:'', redirectTo:'/login',pathMatch:'full'},
  {path:'login', redirectTo:'/login',pathMatch:'full'},
  {path:'panel', redirectTo:'/panel',pathMatch:'full'},
];
 

 