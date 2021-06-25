import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../Shared/Services/auth/services/auth.service';
import { Globals } from '../models/enums/Globals';
import { NotificationMessageService } from '../Shared/Services/NotificationMessage.service';

@Injectable({
    providedIn: 'root'
})
export class LoginRedirectGuard implements CanActivate {
  
    constructor(private router: Router, private authService: AuthService, private alertService: NotificationMessageService) {

    }
    canActivate(route: ActivatedRouteSnapshot,state: RouterStateSnapshot):
     Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

        if (!this.authService.isSignIn()) {
            
            return true
        }
        else {
            this.router.navigate(['/panel/dashboard']);
            //this.router.navigate([this.authService.getDashboardUrls()]);
            this.alertService.showMessage('شما قبلا وارد شدید','موفق',Globals.warningMessage)
            return false
        }


    } 
}
