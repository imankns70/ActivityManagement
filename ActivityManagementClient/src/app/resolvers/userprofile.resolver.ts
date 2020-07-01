import { Injectable } from "@angular/core";
import { User } from '../models/user';
import { UserService } from '../components/panel/services/user.service';
import { Resolve, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { NotificationMessageService } from '../Services/NotificationMessage.service';
import { Observable, of, empty } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Globals } from '../models/enums/Globals';
import { ApiResult } from '../models/apiresult';

@Injectable({ providedIn: 'root' })
export class UserProfileResolver implements Resolve<ApiResult>{
    constructor(private userService: UserService, private router: Router, private alertService: NotificationMessageService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {

        return this.userService.getUsers()

            // .pipe(

            //     catchError(error => {

            //         this.alertService.showMessage(error, "خطا", Globals.errorMessage);
            //         this.router.navigate['/panel/myprofile']
            //         return of(null)
            //     })
            // )
    }
}