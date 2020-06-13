import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { AuthComponent } from './auth/auth.component';

const routes: Routes = [

   { path: 'auth', redirectTo: '/auth/login', pathMatch: 'full' },
   { path: 'auth', loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule) },

   {
      path: 'panel',
      canActivate: [AuthGuard],
      loadChildren: () => import('./panel/panel.module').then(m => m.PanelModule)
   },


   { path: '**', redirectTo: '/auth/login', pathMatch: 'full' },



];

@NgModule({
   imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
   exports: [RouterModule]

})
export class AppRoutingModule {

}