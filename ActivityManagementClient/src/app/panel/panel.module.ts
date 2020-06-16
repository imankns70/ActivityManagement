import { NgModule } from '@angular/core';
import { PanelComponent } from './panel.component';
import { PanelRoutingModule } from './panel-routing.module';
import { NavbarComponent } from './components/navbar/navbar.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { RightSideMenuComponent } from './components/right-side-menu/right-side-menu.component';
import { UserProfileCollapseComponent } from './components/user-profile-collapse/user-profile-collapse.component';

@NgModule({
  imports: [
    PanelRoutingModule
  ],
  declarations: [
    PanelComponent,
    DashboardComponent,
    NavbarComponent,
    RightSideMenuComponent,
    UserProfileComponent,
    UserProfileCollapseComponent

  ],

  providers:[]

})
export class PanelModule {

}
