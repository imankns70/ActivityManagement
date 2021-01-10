import { NgModule } from '@angular/core';
import { PanelComponent } from './panel.component';
import { PanelRoutingModule } from './panel-routing.module';
import { DashboardComponent } from '../panel/components/dashboard/dashboard.component';
import { CommonModule } from '@angular/common';
import { RightSideMenuComponent } from './components/right-side-menu/right-side-menu.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { UserProfileCollapseComponent } from './user-profile-collapse/user-profile-collapse.component';
import { KednoGridService } from 'src/app/Services/kedno-grid.service';


@NgModule({
  imports: [

    PanelRoutingModule,
    CommonModule



  ],

  declarations: [
    PanelComponent,
    DashboardComponent,
    RightSideMenuComponent,
    UserProfileComponent,
    UserProfileCollapseComponent,
  ],
providers:[]


})
export class PanelModule {

}
