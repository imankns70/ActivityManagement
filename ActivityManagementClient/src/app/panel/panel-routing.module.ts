import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PanelComponent } from './panel.component';

const routes: Routes = [

    {
        path: '',
        component: PanelComponent,

    },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]

})
export class PanelRoutingModule {

}