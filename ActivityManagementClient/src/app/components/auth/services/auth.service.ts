import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { ApiResult } from 'src/app/models/apiresult';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'Account/';
  constructor(private http: HttpClient) { }
  login(viewModel: any): Observable<ApiResult> {
    return this.http.post<ApiResult>(this.baseUrl + 'SignIn', viewModel)


  }

  register(viewModel: any): Observable<ApiResult> {
    viewModel.gender == 'مرد' ? viewModel.gender = 1 : viewModel.gender = 2
    return this.http.post<ApiResult>(this.baseUrl + 'Register', viewModel)
  }
  isSignIn(): boolean {
    return localStorage.getItem('token') == null ? false : true;

  }
}
