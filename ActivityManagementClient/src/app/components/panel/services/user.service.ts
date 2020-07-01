import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult } from 'src/app/models/apiresult';
import { setTokenHeader } from 'src/app/Services/customFunction';
import { catchError, map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl + 'UserManager/';
  constructor(private http: HttpClient) { }

  getUsers(): Observable<ApiResult> {
   
    return this.http.get<ApiResult>(this.baseUrl + 'GetUsers', { headers: setTokenHeader() })


  }

  GetUserLoggedIn(): Observable<ApiResult> {
    return this.http.get<ApiResult>(this.baseUrl+'GetUserLoggedIn')

  }
}
