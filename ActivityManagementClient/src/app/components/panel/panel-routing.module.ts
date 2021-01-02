import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PanelComponent } from './panel.component';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { DashboardComponent } from '../panel/components/dashboard/dashboard.component';

const routes: Routes = [

    {
        path: '', component: PanelComponent,
        children: [
            { path: 'dashboard', canActivate: [AuthGuard], component: DashboardComponent },    
        ]

    },
    { path: 'user', component:PanelComponent,
     loadChildren: () => import('../panel/components/user/user.module').then(m => m.UserModule) },

 
    
 
    //{ path: '**', redirectTo: '/auth/login', pathMatch: 'full' },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]

})
export class PanelRoutingModule {

}