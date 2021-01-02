import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { catchError, filter, take, switchMap, tap } from 'rxjs/operators';
import { AuthService } from '../components/auth/services/auth.service';
import { StatusCode } from '../models/enums/StatusCode';
import { Router } from '@angular/router';
import { NotificationMessageService } from './NotificationMessage.service';
import { Globals } from '../models/enums/Globals';
import { environment } from 'src/environments/environment';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  baseUrl = environment.apiUrl + 'Account/';
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  
  constructor(public authService: AuthService, private route: Router, private alertService: NotificationMessageService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

  
    if (request.url.indexOf(this.baseUrl + 'Auth') != 0) {
      if (this.authService.getJwtToken()) {
        request = this.addToken(request, this.authService.getJwtToken());
      }
    }
    

    return next.handle(request).pipe(
      tap((event: HttpEvent<any>) => {
      
        if (event instanceof HttpResponse) {
         
          console.log('success');

        }
      }),
      catchError(error => {
      
        if (error.error instanceof ErrorEvent) {
          // A client-side or network error occurred. Handle it accordingly.
          console.error('An error occurred:', error.error.message);
        }
        if (error instanceof HttpErrorResponse && error.status === 401) {

          if (error.error.StatusCode == StatusCode.unAuthorized) {


            return this.handleRefreshToken(request, next);
          }
          if (error.error.StatusCode == StatusCode.logOut) {
            this.alertService.showMessage('خطا در اعتبار سنجی', 'خطا', Globals.errorMessage)
            this.authService.logout()

          }
        }
        if (error.error.StatusCode == StatusCode.redirectToHome) {
          this.route.navigate(['/panel/dashboard'])

        }

        else {

      
          this.alertService.showMessage(error.error.Message, 'خطا', Globals.errorMessage)
          return throwError(error.error.Message);
        }
      }));
  }

  private addToken(request: HttpRequest<any>, token: string) {
    return request.clone({
      setHeaders: {
        'Authorization': `Bearer ${token}`
      }
    });
  }


  private handleRefreshToken(request: HttpRequest<any>, next: HttpHandler) {
    if (!this.isRefreshing) {
    


      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);

      return this.authService.refreshToken().pipe(

        switchMap((token: any) => {
        
          this.isRefreshing = false;
          this.refreshTokenSubject.next(token.data.accessToken);
          return next.handle(this.addToken(request, token.data.accessToken));
        }));

    } else {
      return this.refreshTokenSubject.pipe(
        filter(token => token != null),
        take(1),
        switchMap(jwt => {
          return next.handle(this.addToken(request, jwt));
        }));
    }
  }
}