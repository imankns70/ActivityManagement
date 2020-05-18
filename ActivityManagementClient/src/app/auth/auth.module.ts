import { NgModule } from '@angular/core';
import { AuthComponent } from './auth.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { authRoutes } from './authRoutes.routing';

@NgModule({
  imports: [
    BrowserModule,
    RouterModule.forRoot(authRoutes)
  ],
  declarations: [
    AuthComponent, 
    LoginComponent, 
    RegisterComponent],

})
export class AuthModule { }
