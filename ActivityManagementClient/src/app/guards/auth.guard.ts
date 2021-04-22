import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../Shared/Services/auth/services/auth.service';
import { Globals } from '../models/enums/Globals';
import { NotificationMessageService } from '../Shared/Services/NotificationMessage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private alert: NotificationMessageService, private router: Router) {

  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    // return current url
    //let stateName: string = state.url.replace('/', '')

    if (this.authService.isSignIn()) {
       
      const roles = next.data['roles'] as Array<string>
      if (roles) {
        const isMatch = this.authService.roleMatch(roles);
        if (isMatch) {
          return true;
        } else {
          this.alert.showMessage('شما به این بخش دسترسی ندارید', 'عدم دسترسی', Globals.errorMessage);
          return  this.router.navigate(['/auth/login']);
          // ریداریکت کدن کاربر به بخش ماژول خودش
          //return  this.router.navigate([this.authService.getDashboardUrls(roles));
          
        

        }
      }
      return true;

    } else {

      this.alert.showMessage('شما به این بخش دسترسی ندارید.', 'عدم دسترسی', Globals.errorMessage);
      return this.router.navigate(['/auth/login'],
        { queryParams: { return: state.url } });
    }



  }
}
