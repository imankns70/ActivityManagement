import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse, HttpResponse } from '@angular/common/http';
 
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { catchError, filter, take, switchMap, map, tap, finalize} from 'rxjs/operators';
import { AuthService } from './auth/services/auth.service';
import { StatusCode } from '../../models/enums/StatusCode';
import { Router } from '@angular/router';
import { NotificationMessageService } from './NotificationMessage.service';
import { Globals } from '../../models/enums/Globals';
import { environment } from 'src/environments/environment';
import { ApiResult } from 'src/app/models/apiresult';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  baseUrl = environment.apiUrl + 'Account/';
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

  constructor(public authService: AuthService, private route: Router, private alertService: NotificationMessageService) { }

  //intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
    if (request.url.indexOf(this.baseUrl + 'Auth') != 0) {
      if (this.authService.getJwtToken()) {
        request = this.attachTokenToRequest(request, this.authService.getJwtToken());
      }
    }

 

  

  return next.handle(request).pipe(
      //filter(e => e.type !== 0),
      tap((event: any) => {
        

        
        if (event instanceof HttpResponse) {

          console.log('success');

        }
       
      
      }),
      catchError((error):Observable<any> => {
        
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
      }),
      
       );

   
  }

  private attachTokenToRequest(request: HttpRequest<any>, token: string) {
     
    return request.clone({setHeaders: { 'Authorization': `Bearer ${token}`} });
  }


  private handleRefreshToken(request: HttpRequest<any>, next: HttpHandler) {

 
    if (!this.isRefreshing) {


      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);

      return this.authService.refreshToken().pipe(

        switchMap((result: ApiResult) => {

          this.isRefreshing = false;
          this.refreshTokenSubject.next(result.data.accessToken);
          return next.handle(this.attachTokenToRequest(request, result.data.accessToken));
        }));

    } else {
      return this.refreshTokenSubject.pipe(
        filter(token => token != null),
        take(1),
        switchMap(jwt => {
          return next.handle(this.attachTokenToRequest(request, jwt));
        }));
    }
  }
}