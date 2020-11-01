import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

const routes: Routes = [

    // path: '', component: PanelComponent,
    // children: [
    //     { path: 'dashboard', canActivate: [AuthGuard], component: DashboardComponent },
        // {
        //     path: 'myprofile', canActivate: [AuthGuard], 
        //     component: MyProfileComponent,
        //     resolve: { user: UserProfileResolver },
        //     canDeactivate: [PreventUnsavedGuard]
        // },

        //{ path: 'role/rolelist', canActivate: [AuthGuard], component: RoleListComponent }
    ]

];

@NgModule({
   imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
   exports: [RouterModule]

})
export class TeamTitleRoutingModule {

}