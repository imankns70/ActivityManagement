import {

  HttpEvent,

  HttpInterceptor,

  HttpHandler,

  HttpRequest,

  HttpResponse,

  HttpErrorResponse

} from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable, throwError } from 'rxjs';

import { retry, catchError } from 'rxjs/operators';
import { StatusCode } from '../models/enums/StatusCode';
import { ApiResult } from '../models/apiresult';
import { AuthService } from '../components/auth/services/auth.service';
@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {

  }
  apiResult: ApiResult = new ApiResult()
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<ApiResult>> {
    request = request.clone({
      setHeaders: {
        'Content-Type': 'application/json; charset=utf-8',
        'Accept': 'application/json',
        'Authorization': `Bearer ${this.authService.getToken()}`,
      },
    });

    return next.handle(request) 

      .pipe(
      retry(1),
      catchError((error: HttpErrorResponse) => {

        if (error.error instanceof ErrorEvent) {
          //client side errors

          this.apiResult.isSuccess = false;
          this.apiResult.message.push('خطا در کلاینت');

        } else {
          // server side errors
          switch (error.error.StatusCode) {
            case StatusCode.serverError:

              this.apiResult.isSuccess = false;
              this.apiResult.statusCode = error.error.StatusCode;
              this.apiResult.message.push(this.apiResult.message.join(','));
              break;
            case StatusCode.notFound:
              this.apiResult.isSuccess = false;
              this.apiResult.statusCode = error.error.StatusCode;
              this.apiResult.message.push(this.apiResult.message.join(','));
              break;
            case StatusCode.unAuthorized:
              this.authService.logout();
              this.apiResult.isSuccess = false;
              this.apiResult.statusCode = error.error.StatusCode;
              this.apiResult.message.push(this.apiResult.message.join(','));
              break;
          }

        }

        return throwError(this.apiResult)
      })
    );
  }
}