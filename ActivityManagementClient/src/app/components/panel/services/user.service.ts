import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult } from 'src/app/models/apiresult';
import { User } from 'src/app/models/user';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl + 'UserManager/';

  constructor(private http: HttpClient) { }

  getUsers(): Observable<any> {

    return this.http.get<any>(this.baseUrl + 'GetUsers')


  }

  GetUserLoggedIn(): Observable<ApiResult> {
    return this.http.get<ApiResult>(this.baseUrl + 'GetUserLoggedIn')

  }

  updateMyProfile(viewModel: User): Observable<ApiResult> {
    return this.http.post<ApiResult>(this.baseUrl + 'UpdateUserProfile' , viewModel)
  }

}
