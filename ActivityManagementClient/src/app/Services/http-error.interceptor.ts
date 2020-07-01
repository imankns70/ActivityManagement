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

    public apiResult: ApiResult;
 
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<ApiResult>> {

        return next.handle(request)

            .pipe(
                retry(1),
                catchError((error: HttpErrorResponse) => {
           
                    if (error.error instanceof ErrorEvent) {
                        //client side errors
                        this.apiResult.isSuccess = false;
                        this.apiResult.statusCode = StatusCode.ClientError;
                        this.apiResult.message.push(error.error.message);
                    } else {
                        // server side errors
                        switch (error.error.StatusCode) {
                            case StatusCode.serverError:
                                this.apiResult.isSuccess = false;
                                this.apiResult.statusCode = error.error.StatusCode;
                                this.apiResult.message.push('خطا در سرور');
                                //this.alertService.showMessage('خطا در سرور', 'خطا', Globals.errorMessage)
                                break;
                            case StatusCode.notFound:
                                this.apiResult.isSuccess = false;
                                this.apiResult.statusCode = error.error.StatusCode;
                                this.apiResult.message.push('موردی یافت نشد');
                                //this.alertService.showMessage('موردی یافت نشد', 'خطا', Globals.errorMessage)
                                break;
                            case StatusCode.unAuthorized:
                                this.apiResult.isSuccess = false;
                                this.apiResult.statusCode = error.error.StatusCode;
                                this.apiResult.message.push('عدم دسترسی');
                                break;
                        }

                    }

                    debugger;
                    return throwError(this.apiResult)
                })
            )
    }
}