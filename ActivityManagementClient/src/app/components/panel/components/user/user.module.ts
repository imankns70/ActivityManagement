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
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { KendoModule } from 'src/app/Shared/Modules/Kendo.module';
import { UserListComponent } from './components/user-list/user-list.component';
import { UsersBindingDirective } from './components/user-list/users-binding.directive';
import { UserGridService } from './services/User.Grid.service';
import { RenderUserComponent } from './components/render-user/render-user.component';
import { NgPersianDatepickerModule } from 'ng-persian-datepicker';
import { DeleteUserComponent } from './components/delete-user/delete-user.component';
@NgModule({
  imports: [
    UserRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    FileUploadModule,
    KendoModule,
    NgPersianDatepickerModule
 
   
  ],
  declarations: [
    UserComponent,
    MyProfileComponent,
    ChangePicComponent,
    UserListComponent,
    UsersBindingDirective,
    RenderUserComponent,
    DeleteUserComponent
    
    
  ],
   
  providers: [
    UserService,
    UserProfileResolver,
    PreventUnsavedGuard,
    UserGridService,
  ]
})
export class UserModule { }
