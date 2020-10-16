import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AuthService } from '../components/auth/services/auth.service';

@Injectable({
    providedIn: 'root'
})
export class JwtInterCeptor implements HttpInterceptor {

    private isTokenRefreshing = false;
    tokenSubject = new BehaviorSubject<string>(null);

    constructor(private authService: AuthService) {

    }
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(this.attachTokenToRequest(req)).pipe(
            tap((event:HttpEvent<any>) => {

                if (event instanceof HttpResponse ) {
                    console.log('success');
                    
                }
            })
        )
    }

    private attachTokenToRequest(req: HttpRequest<any>) {
        return req.clone({
            setHeaders: {
                Authorization: `Bearer ${this.authService.getToken()}`,
            },
        })
    } 
}