import {

    HttpEvent,

    HttpInterceptor,

    HttpHandler,

    HttpRequest,

    HttpResponse,

    HttpErrorResponse,
    HTTP_INTERCEPTORS

} from '@angular/common/http';

import { BehaviorSubject, Observable, throwError } from 'rxjs';

import { retry, catchError, tap, switchMap, finalize, filter, take, switchMapTo } from 'rxjs/operators';
import { StatusCode } from '../models/enums/StatusCode';
import { ApiResult } from '../models/apiresult';
import { AuthService } from '../components/auth/services/auth.service';
import { NotificationMessageService } from './NotificationMessage.service';
import { Router } from '@angular/router';


export class HttpErrorInterceptor implements HttpInterceptor {
    private isTokenRefreshing = false;
    tokenSubject = new BehaviorSubject<string>(null);

    constructor(private authService: AuthService,
        private alertService: NotificationMessageService, private route: Router) {

    }
<<<<<<< HEAD
=======

>>>>>>> 86d7693b2278186bc08513b4eaa76125e75497b8
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        return next.handle(this.attachTokenToRequest(request)).pipe(
            tap((event: HttpEvent<any>) => {

                if (event instanceof HttpResponse) {
                    console.log('success');

                }
            }),
            catchError((error: HttpErrorResponse) => {



                return this.handleError(error, request, next);
            })
        )

    }
    private HandleHttpResponseError(request: HttpRequest<any>, next: HttpHandler) {

        if (!this.isTokenRefreshing) {
            this.isTokenRefreshing = true;
            this.tokenSubject.next(null);

            return this.authService.getNewRefreshToken().pipe(
                switchMap((tokenResponse: any) => {
<<<<<<< HEAD
                    this.tokenSubject.next(tokenResponse.AccessToken);
                    localStorage.setItem('token', tokenResponse.AccessToken);
                    //localStorage.setItem('refreshToken', tokenResponse.RefreshToken);
                    //localStorage.setItem('user', JSON.stringify(tokenResponse.User));
                    return next.handle(this.attachTokenToRequest(request))
                }),catchError(err => {

=======
                    if (tokenResponse) {
                        this.tokenSubject.next(tokenResponse.AccessToken);
                        localStorage.setItem('token', tokenResponse.AccessToken);
                        localStorage.setItem('refreshToken', tokenResponse.RefreshToken);
                        localStorage.setItem('user', JSON.stringify(tokenResponse.User));
                        return next.handle(this.attachTokenToRequest(request))
                    }
                    return this.authService.logout() as any
                }), catchError(err => {
                    this.authService.logout()
                    return this.handleError(err, request, next)

                }), finalize(() => {

                    this.isTokenRefreshing = false;
>>>>>>> 86d7693b2278186bc08513b4eaa76125e75497b8
                })
            );

        }
        else {
<<<<<<< HEAD
            return  this.authService.logout() as any;
           
=======
            this.isTokenRefreshing = false;

            return this.tokenSubject.pipe(
                filter(token => token != null),
                take(1),
                switchMap(token => {
                    return next.handle(this.attachTokenToRequest(request))
                })

            )
>>>>>>> 86d7693b2278186bc08513b4eaa76125e75497b8
        }

    }
    private attachTokenToRequest(req: HttpRequest<any>) {
        return req.clone({
            setHeaders: {
                Authorization: `Bearer ${this.authService.getToken()}`,
            },
        })
    }
<<<<<<< HEAD
private handError(http:HttpErrorResponse){
=======
    private handleError(error: HttpErrorResponse, request: HttpRequest<any>, next: HttpHandler) {
        let message: string = '';
        if (error.error instanceof ErrorEvent) {
            //client side errors
            console.log('client side errors occured');

        } else {
            // server side errors
            switch (error.error.StatusCode) {
                case StatusCode.serverError:

                    message = error.message

                    break;
                case StatusCode.unAuthorized:
                    console.log('attempting refresh token ...');
                    this.HandleHttpResponseError(request, next);
                    break;
                case StatusCode.notFound:
                    this.route.navigate(['/auth/login'])
                    this.authService.logout()
                    message = error.message
                    break;

                case StatusCode.badRequest:
                    this.route.navigate(['/auth/login'])
                    this.authService.logout()
                    message = error.message
                    break;

                case StatusCode.listEmpty:
                    this.route.navigate(['/auth/login'])
                    this.authService.logout()
                    break;

                case StatusCode.logicError:
                    this.route.navigate(['/auth/login'])
                    this.authService.logout()
                    message = error.message
                    break;



            }

        }

        return throwError(message)
    }

>>>>>>> 86d7693b2278186bc08513b4eaa76125e75497b8

}
}
export const ErrorInterceptorPrivider = {
    provide: HTTP_INTERCEPTORS,
    useClass: HttpErrorInterceptor,
    multi: true
}