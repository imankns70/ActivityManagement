import { NgModule } from '@angular/core';
import { PanelComponent } from './panel.component';
import { PanelRoutingModule } from './panel-routing.module';
import { DashboardComponent } from '../panel/dashboard/dashboard.component';
import { CommonModule } from '@angular/common';
import { FileUploadModule } from 'ng2-file-upload';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RightSideMenuComponent } from './right-side-menu/right-side-menu.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { UserProfileCollapseComponent } from './user-profile-collapse/user-profile-collapse.component';


@NgModule({
  imports: [

    PanelRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule
 


  ],

  declarations: [
    PanelComponent,
    DashboardComponent,
    UserProfileComponent,
    UserProfileCollapseComponent,
    RightSideMenuComponent,
  ],



})
export class PanelModule {

}
