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
import { UserBindigDirective } from './user-list/user-bindig.directive';
import { CreateUserComponent } from './user-list/create/create-user/create-user.component';

@NgModule({
  imports: [
    UserRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    FileUploadModule,
    KendoModule
   
  ],
  entryComponents: [CreateUserComponent],
  declarations: [
    UserComponent,
    MyProfileComponent,
    ChangePicComponent,
    UserListComponent,
    UserBindigDirective,
    CreateUserComponent
    
  ],
  providers: [
    UserService,
    UserProfileResolver,
    PreventUnsavedGuard,
    AuthGuard,
  ]
})
export class UserModule { }
