import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginRedirectGuard } from 'src/app/guards/login-redirect.guard';
import { AuthComponent } from './auth.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
 
const routes: Routes = [
   
   {
      path: '',
      component: AuthComponent,
      children: [
         { path: 'login',canActivate:[LoginRedirectGuard],
          component: LoginComponent },
         { path: 'register', component: RegisterComponent }
      ]
   },
 
];

@NgModule({
   imports: [RouterModule.forChild(routes)],
   exports: [RouterModule]

})
export class AuthRoutingModule {

}