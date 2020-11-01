import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { PreventUnsavedGuard } from 'src/app/guards/prevent-unsaved.guard';
import { UserProfileResolver } from 'src/app/resolvers/userprofile.resolver';
import { MyProfileComponent } from './my-profile/my-profile.component';

const routes: Routes = [


    {
        path: 'myprofile', canActivate: [AuthGuard],
        component: MyProfileComponent,
        resolve: { user: UserProfileResolver },
        canDeactivate: [PreventUnsavedGuard]
    },



];

@NgModule({
    imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
    exports: [RouterModule]

})
export class UserRoutingModule {

}