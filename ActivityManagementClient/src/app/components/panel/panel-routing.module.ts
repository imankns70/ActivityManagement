import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PanelComponent } from './panel.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { MyProfileComponent } from './components/my-profile/my-profile.component';
import { UserProfileResolver } from 'src/app/resolvers/userprofile.resolver';
import { PreventUnsavedGuard } from 'src/app/guards/prevent-unsaved.guard';
import { RoleListComponent } from './components/role/role-list/role-list.component';

const routes: Routes = [

    {
        path: '', component: PanelComponent,
        children: [
            { path: 'dashboard', component: DashboardComponent },
            { path: 'myprofile', component: MyProfileComponent,
             resolve: { user: UserProfileResolver },
             canDeactivate:[PreventUnsavedGuard]
             },

             {path:'role/rolelist',component:RoleListComponent}
        ]

    },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]

})
export class PanelRoutingModule {

}