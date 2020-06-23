import { NgModule } from '@angular/core';
import { PanelComponent } from './panel.component';
import { PanelRoutingModule } from './panel-routing.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { RightSideMenuComponent } from './components/right-side-menu/right-side-menu.component';
import { UserProfileCollapseComponent } from './components/user-profile-collapse/user-profile-collapse.component';
import { MyProfileComponent } from './components/my-profile/my-profile.component';
import { UserService } from './services/user.service';
import { JwtModule } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
export function tokenGetter() {
  return localStorage.getItem('token')
}
@NgModule({
  imports: [
    PanelRoutingModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: [
          environment.apiUrl + 'api/v1/UserManager',
        ],
        blacklistedRoutes: [
          environment.apiUrl + 'api/v1/Account/SignIn',
          environment.apiUrl + 'api/v1/Account/Register']
      }
    })
  ],
  declarations: [
    PanelComponent,
    DashboardComponent,
    RightSideMenuComponent,
    UserProfileComponent,
    UserProfileCollapseComponent,
    MyProfileComponent

  ],

  providers: [UserService]

})
export class PanelModule {

}
