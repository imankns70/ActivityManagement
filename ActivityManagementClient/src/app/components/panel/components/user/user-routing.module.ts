import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { PreventUnsavedGuard } from 'src/app/guards/prevent-unsaved.guard';
import { UserProfileResolver } from 'src/app/resolvers/userprofile.resolver';
import { MyProfileComponent } from '../user/components/my-profile/my-profile.component';
import { UserListComponent } from './components/user-list/user-list.component';
import { UserComponent } from './user.component';

const routes: Routes = [

    {
        path: '', component: UserComponent,
        children: [
            {
                path: 'myprofile', canActivate: [AuthGuard],
                data: { title: ['پروفایل شخصی'] },
                component: MyProfileComponent,
                resolve: { user: UserProfileResolver },
                canDeactivate: [PreventUnsavedGuard]
            },
            {
                path: 'userlist', canActivate: [AuthGuard],

                data: { roles: ['Admin'], title: ['لیست کاربران'] },
                component: UserListComponent,
            }

        ]
    }





];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]

})
export class UserRoutingModule {

}