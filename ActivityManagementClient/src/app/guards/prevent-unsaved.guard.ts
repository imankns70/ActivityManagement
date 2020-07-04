import { Injectable } from "@angular/core";
import { MyProfileComponent } from '../components/panel/components/my-profile/my-profile.component';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable()
export class PreventUnsavedGuard implements CanDeactivate<MyProfileComponent> {

    canDeactivate(
        component: MyProfileComponent,
        currentRoute: ActivatedRouteSnapshot,
        currentState: RouterStateSnapshot,
        nextState: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
 
        if (component.editForm.dirty) {

            return confirm('شما تغییراتی ایجاد کرده اید با خروج از آن تغییرات ذخیره نمی شوند');

        } 
        return true;

    }
}