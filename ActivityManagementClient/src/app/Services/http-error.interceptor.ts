import {

    HttpEvent,

    HttpInterceptor,

    HttpHandler,

    HttpRequest,

    HttpResponse,

    HttpErrorResponse

} from '@angular/common/http';

import { Observable, throwError } from 'rxjs';

import { retry, catchError } from 'rxjs/operators';
import { StatusCode } from '../models/enums/StatusCode';
import { ApiResult } from '../models/apiresult';


export class HttpErrorInterceptor implements HttpInterceptor {

    apiResult: ApiResult = new ApiResult()
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<ApiResult>> {

        return next.handle(request)

            .pipe(
                retry(1),
                catchError((error: HttpErrorResponse) => {
                    debugger;
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
                                this.apiResult.message.push('خطا در سرور');
                                break;
                            case StatusCode.notFound:
                                this.apiResult.isSuccess = false;
                                this.apiResult.statusCode = error.error.StatusCode;
                                this.apiResult.message.push('موردی یافت نشد');
                                break;
                            case StatusCode.unAuthorized:
                                this.apiResult.isSuccess = false;
                                this.apiResult.statusCode = error.error.StatusCode;
                                this.apiResult.message.push('عدم دسترسی');
                                break;
                        }

                    }

                    return throwError(this.apiResult)
                })
            )
    }
}