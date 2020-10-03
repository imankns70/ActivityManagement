import { NgModule } from '@angular/core';
import { PanelComponent } from './panel.component';
import { PanelRoutingModule } from './panel-routing.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { RightSideMenuComponent } from './components/right-side-menu/right-side-menu.component';
import { UserProfileCollapseComponent } from './components/user-profile-collapse/user-profile-collapse.component';
import { MyProfileComponent } from './components/my-profile/my-profile.component';
import { UserService } from './services/user.service';
import { UserProfileResolver } from 'src/app/resolvers/userprofile.resolver';
import { PreventUnsavedGuard } from 'src/app/guards/prevent-unsaved.guard';
import { ChangePicComponent } from './components/my-profile/components/change-pic/change-pic.component';
import { CommonModule } from '@angular/common';
import { FileUploadModule  } from 'ng2-file-upload';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RoleListComponent } from './components/role/role-list/role-list.component';
import { AuthGuard } from 'src/app/guards/auth.guard';


@NgModule({
  imports: [
   
    PanelRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    FileUploadModule
    

  ],

  declarations: [
    PanelComponent,
    DashboardComponent,
    RightSideMenuComponent,
    UserProfileComponent,
    UserProfileCollapseComponent,
    MyProfileComponent,
    ChangePicComponent,
    RoleListComponent

  ],

  providers: [
    UserService,
    UserProfileResolver,
    PreventUnsavedGuard,
    AuthGuard,
  ]

})
export class PanelModule {

}
