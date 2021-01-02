import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from './user.component';
import { MyProfileComponent } from '../user/components/my-profile/my-profile.component';
import { UserService } from '../../services/user.service';
import { UserProfileResolver } from 'src/app/resolvers/userprofile.resolver';
import { PreventUnsavedGuard } from 'src/app/guards/prevent-unsaved.guard';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { ChangePicComponent } from '../user/components/change-pic/change-pic.component';
import { UserRoutingModule } from './user-routing.module';
import { FileUploadModule } from 'ng2-file-upload';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { KendoModule } from 'src/app/SharedModules/Kendo/Kendo.module';
import { UserListComponent } from './user-list/user-list.component';
import { CreateUserComponent } from './user-list/create/create-user/create-user.component';
import { UserGridService } from '../../services/User.Grid.service';

@NgModule({
  imports: [
    UserRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    FileUploadModule,
    KendoModule
   
  ],
  declarations: [
    UserComponent,
    MyProfileComponent,
    ChangePicComponent,
    UserListComponent,
    CreateUserComponent
    
  ],
  entryComponents: [CreateUserComponent],
  providers: [
    UserService,
    UserGridService,
    UserProfileResolver,
    PreventUnsavedGuard,
    AuthGuard,
  ]
})
export class UserModule { }
