import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from "@auth0/angular-jwt";


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:9788/api/v1/Account/';
  jwtHelper = new JwtHelperService();
  constructor(private http: HttpClient) { }
  login(viewModel: any) {
    return this.http.post(this.baseUrl + 'SignIn', viewModel).pipe(
      map((resp: any) => {

        const apiResult = resp;
        if (apiResult.isSuccess)

          localStorage.setItem('token', apiResult.data)
        return apiResult
      })
    );
  }

  register(viewModel: any) {
    viewModel.gender == 'مرد' ? viewModel.gender = 1 : viewModel.gender = 2
    return this.http.post(this.baseUrl + 'Register', viewModel).pipe(
      map((resp: any) => {
        const apiResult = resp;

        return apiResult;
      })
    )
  }
  isSignIn(): boolean {
    return false;
  }
}
