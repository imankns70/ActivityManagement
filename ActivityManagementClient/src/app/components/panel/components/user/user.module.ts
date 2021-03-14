import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from './user.component';
import { MyProfileComponent } from '../user/components/my-profile/my-profile.component';
import { UserService } from '../../services/user.service';
import { UserProfileResolver } from 'src/app/resolvers/userprofile.resolver';
import { PreventUnsavedGuard } from 'src/app/guards/prevent-unsaved.guard';
import { ChangePicComponent } from '../user/components/change-pic/change-pic.component';
import { UserRoutingModule } from './user-routing.module';
import { FileUploadModule } from 'ng2-file-upload';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { KendoModule } from 'src/app/SharedModules/Kendo/Kendo.module';
import { UserListComponent } from './components/user-list/user-list.component';
import { CreateUserComponent } from './components/create-user/create-user.component';
import { UsersBindingDirective } from './components/user-list/users-binding.directive';
import { UserGridService } from './services/User.Grid.service';
import { EditUserComponent } from './components/edit-user/edit-user.component';
import { DpDatePickerModule } from 'ng2-jalali-date-picker';

@NgModule({
  imports: [
    UserRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    FileUploadModule,
    KendoModule,
    DpDatePickerModule
   
  ],
  declarations: [
    UserComponent,
    MyProfileComponent,
    ChangePicComponent,
    UserListComponent,
    UsersBindingDirective,
    CreateUserComponent,
    CreateUserComponent, 
    EditUserComponent
    
  ],
   
  providers: [
    UserService,
    UserProfileResolver,
    PreventUnsavedGuard,
    UserGridService,
  ]
})
export class UserModule { }
