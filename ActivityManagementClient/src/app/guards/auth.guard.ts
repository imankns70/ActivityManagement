import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../auth/services/auth.service';
import { NotificationMessageService } from '../Services/NotificationMessage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private alert: NotificationMessageService,private router:Router) {
   
  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
 debugger;
<<<<<<< HEAD
      if (this.authService.isSignIn) {
=======
      if (this.authService.isSignIn()) {
>>>>>>> 732bc7dfee2e41b662487679b8dc3c075e09de7d
        return true;
  
      } else {
      return  this.router.navigate(['/auth/login'])
      }
    
  }

}
