import { NgModule } from '@angular/core';
import { LoginComponent } from './login.component';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { loginRoutes } from './loginRoutes.routing';

@NgModule({
  imports: [
    BrowserModule,
    RouterModule.forRoot(loginRoutes)
  ],
  declarations: [LoginComponent],
 
})
export class LoginModule { }
