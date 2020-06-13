import { NgModule } from '@angular/core';
import { AuthComponent } from './auth.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AuthRoutingModule } from './auth-routing.module';
import { FormsModule } from '@angular/forms';
import { AuthService } from './services/auth.service';
import { BrowserModule } from '@angular/platform-browser';
@NgModule({
  imports: [
    AuthRoutingModule,
    FormsModule,
  ],
  declarations: [
    AuthComponent,
    LoginComponent,
    RegisterComponent],
  providers: [AuthService]

})
export class AuthModule { }
