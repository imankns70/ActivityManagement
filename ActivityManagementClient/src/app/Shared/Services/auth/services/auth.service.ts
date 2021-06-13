import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, BehaviorSubject } from 'rxjs';
import { ApiResult } from 'src/app/models/apiresult';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user/user';
import { tap } from 'rxjs/operators';
import { Store } from '@ngrx/store';
import * as fromStore from 'src/app/store'

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'Account/';
  roles: Array<string>;
  currentUser: User;
  constructor(private router: Router, private http: HttpClient,
    private generalStore: Store<fromStore.State>) {

debugger;
    this.generalStore.select(fromStore.getUserLoggedState).subscribe(user => {
      debugger;
      this.currentUser = user
    });
  }



  login(requestToken: any): Observable<ApiResult> {

    return this.http.post<ApiResult>(this.baseUrl + 'Auth', requestToken).pipe(

      tap((resp: ApiResult) => {

        if (resp.isSuccess) {

          this.generalStore.dispatch(new fromStore.EditLoggedUser(resp.data.user));

          localStorage.setItem('token', resp.data.accessToken);
          localStorage.setItem('refreshToken', resp.data.refreshToken);

        } else {
          console.log(resp.message);

        }


      }
      )
    );

  }

  register(viewModel: any): Observable<ApiResult> {
    viewModel.gender == 'male' ? viewModel.gender = 1 : viewModel.gender = 2
    return this.http.post<ApiResult>(this.baseUrl + 'Register', viewModel)
  }
  isSignIn(): boolean {
    return this.getJwtToken() !== '';

  }
  getJwtToken(): string {
    return localStorage.getItem('token')
  }
  logout() {

    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
    this.generalStore.dispatch(new fromStore.ResetLoggedUser());
    this.roles = [];
    this.router.navigate(['/auth/login'])
  }

  private getRefreshToken() {
    return localStorage.getItem('refreshToken');
  }

  refreshToken() {

    debugger;
    const requestToken = {
      userName: this.currentUser.userName,
      refreshToken: this.getRefreshToken(),
      grantType: 'RefreshToken'
    }
    return this.http.post<any>(this.baseUrl + 'Auth', requestToken).pipe(
      tap((res: ApiResult) => {
        this.storeJwtToken(res.data.accessToken);
        this.generalStore.dispatch(new fromStore.EditLoggedUser(res.data.user));
        this.roles = res.data.roles
      }));
  }


  private storeJwtToken(jwt: string) {
    localStorage.setItem('token', jwt);
  }


  roleMatch(allowedRoles): boolean {
    debugger;
    let isMatch = false;

    if (this.currentUser.id != 0) {

      this.roles = this.currentUser.roles as Array<string>
    }
    else {
      this.generalStore.dispatch(new fromStore.LoadLoggedUser());
      this.generalStore.select(fromStore.getUserLoggedState).subscribe(user => {
        this.currentUser = user

      })
    }

    if (Array.isArray(this.roles)) {

      allowedRoles.forEach(element => {
        if (this.roles.includes(element)) {
          isMatch = true;
        }
      });

    } else {

      allowedRoles.forEach(element => {
        if (this.roles === element) {
          isMatch = true;
        }
      });
    }


    return isMatch;

  }

  getDashboardUrls(): string {
    let url = '';

    this.roles = this.currentUser.roles as Array<string>

    if (this.roles.length != 0) {


      if (this.roles.includes('Admin')) {
        url = '/panel/dashboard';
      }

      if (this.roles.includes('user')) {
        url = '/panel/dashboard';
      }

    }
    else {
      url = '/auth/login';

    }
    return url;

  }
  getUser(): any {
    if (this.currentUser.id === 0) {
      this.generalStore.dispatch(new fromStore.LoadLoggedUser())
    }

    return this.currentUser;
  }


}