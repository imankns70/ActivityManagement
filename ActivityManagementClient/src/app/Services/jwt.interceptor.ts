import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from 'rxjs';
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
        return next.handle(this.attachTokenToRequest(req))
    }

    private attachTokenToRequest(req: HttpRequest<any>) {
        return req.clone({
            setHeaders: {
                Authorization: `Bearer ${this.authService.getToken()}`,
            },
        })
    } 
}