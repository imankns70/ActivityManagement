import { NgModule } from '@angular/core';
import { PanelComponent } from './panel.component';
import { PanelRoutingModule } from './panel-routing.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { RightSideMenuComponent } from './components/right-side-menu/right-side-menu.component';
import { UserProfileCollapseComponent } from './components/user-profile-collapse/user-profile-collapse.component';
import { MyProfileComponent } from './components/my-profile/my-profile.component';

@NgModule({
  imports: [
    PanelRoutingModule
  ],
  declarations: [
    PanelComponent,
    DashboardComponent,
    RightSideMenuComponent,
    UserProfileComponent,
    UserProfileCollapseComponent,
    MyProfileComponent

  ],

  providers:[]

})
export class PanelModule {

}
