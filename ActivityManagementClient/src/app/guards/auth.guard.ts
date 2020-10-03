import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../components/auth/services/auth.service';
import { NotificationMessageService } from '../Services/NotificationMessage.service';

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

      return true;

    } else {
      return this.router.navigate(['/auth/login'], { queryParams: { return: state.url } });
    }



  }
}
