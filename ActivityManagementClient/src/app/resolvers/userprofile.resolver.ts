import { Injectable } from "@angular/core";
import { UserService } from '../components/panel/services/user.service';
import { Resolve, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { NotificationMessageService } from '../Shared/Services/NotificationMessage.service';
import { Observable, of, empty } from 'rxjs';
 import { ApiResult } from '../models/apiresult';

@Injectable({ providedIn: 'root' })
export class UserProfileResolver implements Resolve<ApiResult>{
    constructor(private userService: UserService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {

        return this.userService.GetUserLoggedIn()

            // .pipe(
             
            // catchError(error => {

            //     this.alertService.showMessage(error.message, "خطا", Globals.errorMessage);
            //     this.router.navigate['/panel/myprofile']
            //     return of(null)
            // })
            // )
    }
}