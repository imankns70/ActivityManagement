import { Injectable } from "@angular/core";
import { User } from '../models/user';
import { UserService } from '../components/panel/services/user.service';
import { Resolve, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { NotificationMessageService } from '../Services/NotificationMessage.service';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Globals } from '../models/enums/Globals';

@Injectable()
export class UserProfileResolver implements Resolve<User>{
    constructor(private userService: UserService, private router: Router, private alertService: NotificationMessageService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {

        this.userService.GetUserLoggedIn().pipe(
        
            catchError(error => {
                console.log(error); 
                this.alertService.showMessage(error, "خطا", Globals.errorMessage);
                this.router.navigate['/panle/myprofile']
                return of(null)
            })
        )
    }
}