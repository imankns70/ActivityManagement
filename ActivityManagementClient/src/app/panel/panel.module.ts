import { NgModule } from '@angular/core';
import { PanelComponent } from './panel.component';
import { BrowserModule } from '@angular/platform-browser';
// import { RightSideMenuComponent } from './right-side-menu/right-side-menu.component';
// import { UserProfileComponent } from './user-profile/user-profile.component';
// import { UserProfileCollapseComponent } from './user-profile-collapse/user-profile-collapse.component';
import { RouterModule } from '@angular/router';
import { panelRoutes } from './panelroutes.routing';

@NgModule({
  imports: [
    BrowserModule,
    // RightSideMenuComponent,
    // UserProfileComponent,
    // UserProfileCollapseComponent,
    RouterModule.forRoot(panelRoutes)
         
 ],
 declarations: [PanelComponent]
  
})
export class PanelModule { 
  
}
