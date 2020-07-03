import { NgModule } from '@angular/core';
import { AuthComponent } from '../auth/auth.component';
import { LoginComponent } from '../auth/components/login/login.component';
import { RegisterComponent } from '../auth/components/register/register.component';
import { AuthRoutingModule } from './auth-routing.module';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../auth/services/auth.service';
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
