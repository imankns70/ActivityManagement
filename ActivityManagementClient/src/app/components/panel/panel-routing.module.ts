import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PanelComponent } from './panel.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { MyProfileComponent } from './components/my-profile/my-profile.component';

const routes: Routes = [

    {
        path: '',component: PanelComponent,
        children:[
            {path:'dashboard', component:DashboardComponent},
            {path:'myprofile', component:MyProfileComponent}
        ]

    },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]

})
export class PanelRoutingModule {

}