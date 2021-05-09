import { NgModule } from '@angular/core';
import { PanelComponent } from './panel.component';
import { PanelRoutingModule } from './panel-routing.module';
import { DashboardComponent } from '../panel/components/dashboard/dashboard.component';
import { CommonModule } from '@angular/common';
import { RightSideMenuComponent } from './components/right-side-menu/right-side-menu.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { UserProfileCollapseComponent } from './user-profile-collapse/user-profile-collapse.component';
import { SharedService } from 'src/app/Shared/Services/shared-service';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { HasRoleDirective } from 'src/app/directives/hasRole.directive';
import { StoreModule } from '@ngrx/store';
import { helloMessageReducer } from 'src/app/store/helloWorld/helloMessage.reducer';
 



@NgModule({
  imports: [

    PanelRoutingModule,
    CommonModule,
    StoreModule.forRoot({helloMessage:helloMessageReducer}),


  ],

  declarations: [
    PanelComponent,
    DashboardComponent,
    RightSideMenuComponent,
    UserProfileComponent,
    UserProfileCollapseComponent,
    HasRoleDirective
  ],
providers:[SharedService,
  AuthGuard 
]


})
export class PanelModule {

}
