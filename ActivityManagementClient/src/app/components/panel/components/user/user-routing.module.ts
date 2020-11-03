import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { PreventUnsavedGuard } from 'src/app/guards/prevent-unsaved.guard';
import { UserProfileResolver } from 'src/app/resolvers/userprofile.resolver';
import { ChangePicComponent } from '../user/components/change-pic/change-pic.component';
import { MyProfileComponent } from '../user/components/my-profile/my-profile.component';
import { UserComponent } from './user.component';

const routes: Routes = [

    {
        path: '', component: UserComponent,
        children: [


            {
                path: 'myprofile', canActivate: [AuthGuard],
                component: MyProfileComponent,
                resolve: { user: UserProfileResolver },
                canDeactivate: [PreventUnsavedGuard]
            },
            {
                path: 'changepic', component: ChangePicComponent,

            },
        ]
    }





];

@NgModule({
    imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
    exports: [RouterModule]

})
export class UserRoutingModule {

}