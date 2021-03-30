import {

    HttpEvent,

    HttpInterceptor,



    HttpRequest,

    HttpResponse,

    HttpErrorResponse,
    HTTP_INTERCEPTORS

} from '@angular/common/http';

import { BehaviorSubject, Observable, throwError } from 'rxjs';

import { catchError, tap, switchMap, finalize, filter, take } from 'rxjs/operators';
import { StatusCode } from '../../models/enums/StatusCode';
import { AuthService } from './auth/services/auth.service';
import { NotificationMessageService } from './NotificationMessage.service';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { HttpHandler } from '@angular/common/http';
@Injectable({
    providedIn: 'root'
})

export class HttpErrorInterceptor implements HttpInterceptor {
    private isTokenRefreshing = false;
    private tokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

    constructor(private authService: AuthService,
        private alertService: NotificationMessageService, private route: Router) {

    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        debugger;
        return next.handle(this.attachTokenToRequest(request)).pipe(
            tap((event: HttpEvent<any>) => {

                if (event instanceof HttpResponse) {
                    console.log('success');

                }
            }),
            catchError((error): Observable<any> => {


                return this.handleError(error, request, next);
            })
        )

    }

    private HandleHttpResponseError(request: HttpRequest<any>, next: HttpHandler) {

        if (!this.isTokenRefreshing) {
            this.isTokenRefreshing = true;

            this.tokenSubject.next(null);
            // if (!this.isRefreshing) {
            //     this.isRefreshing = true;
            //     this.refreshTokenSubject.next(null);

            //     return this.authService.refreshToken().pipe(
            //       switchMap((token: any) => {
            //         this.isRefreshing = false;
            //         this.refreshTokenSubject.next(token.jwt);
            //         return next.handle(this.addToken(request, token.jwt));
            //       }));

            //   }

            return this.authService.refreshToken().subscribe(nex => {


                debugger;
                switchMap((tokenResponse: any) => {
                    debugger;
                    if (tokenResponse) {
                        debugger
                        this.tokenSubject.next(tokenResponse.accessToken);
                        localStorage.setItem('token', tokenResponse.accessToken);
                        return next.handle(this.attachTokenToRequest(request))

                    }

                }), catchError(err => {

                    return this.handleError(err, request, next)

                }), finalize(() => {

                    this.isTokenRefreshing = false;
                })
            }
            );

        }
        else {
            this.isTokenRefreshing = false;

            return this.tokenSubject.pipe(
                filter(token => token != null),
                take(1),
                switchMap(token => {
                    return next.handle(this.attachTokenToRequest(request))
                }));
        }

    }
    private attachTokenToRequest(req: HttpRequest<any>) {
        const token = this.authService.getJwtToken();
        return req.clone({ setHeaders: { Authorization: 'Bearer ' + token } })
    }
    private handleError(error: HttpErrorResponse, request: HttpRequest<any>, next: HttpHandler) {
        let message: string = '';
        debugger;
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



}
export const ErrorInterceptorPrivider = {
    provide: HTTP_INTERCEPTORS,
    useClass: HttpErrorInterceptor,
    multi: true
}