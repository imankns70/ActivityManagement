import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from './user.component';
import { MyProfileComponent } from './my-profile/my-profile.component';
import { UserService } from '../../services/user.service';
import { UserProfileResolver } from 'src/app/resolvers/userprofile.resolver';
import { PreventUnsavedGuard } from 'src/app/guards/prevent-unsaved.guard';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { ChangePicComponent } from './change-pic/change-pic.component';
import { UserRoutingModule } from './user-routing.module';
import { FileUploadModule } from 'ng2-file-upload';


@NgModule({
  imports: [
    UserRoutingModule,
    CommonModule,
    FileUploadModule

   
  ],
  declarations: [
    UserComponent,
    MyProfileComponent,
    ChangePicComponent
  ],
  providers: [
    UserService,
    UserProfileResolver,
    PreventUnsavedGuard,
    AuthGuard,
  ]
})
export class UserModule { }
