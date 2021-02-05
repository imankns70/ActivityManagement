import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, BehaviorSubject } from 'rxjs';
import { ApiResult } from 'src/app/models/apiresult';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user/user';
import { tap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'Account/';
  currentUser: User;
  imageUrl = new BehaviorSubject<string>('../../../../assets/images/UserPic.png');
  currentPhotoUrl = this.imageUrl.asObservable();
  constructor(private router: Router, private http: HttpClient) { }

  changeUserPhoto(url: string) {

    this.imageUrl.next(url);
  }

  login(requestToken: any): Observable<any> {

    return this.http.post<any>(this.baseUrl + 'Auth', requestToken)

  }

  register(viewModel: any): Observable<ApiResult> {
    viewModel.gender == 'مرد' ? viewModel.gender = 1 : viewModel.gender = 2
    return this.http.post<ApiResult>(this.baseUrl + 'Register', viewModel)
  }
  isSignIn(): boolean {
    return this.getJwtToken() == null ? false : true;

  }
  getJwtToken(): string {
    return localStorage.getItem('token')
  }
  logout() {

    localStorage.removeItem('user');
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
    this.currentUser = null;
    this.router.navigate(['/auth/login'])
  }
  loadUser() {
    const user = JSON.parse(localStorage.getItem('user'));
    if (user) {
      this.currentUser = user;
      this.changeUserPhoto(user.image)
    }

  }
  private getRefreshToken() {
    return localStorage.getItem('refreshToken');
  }

  refreshToken() {

    // const newHeader = new HttpHeaders({
    //   Authorization:  `Bearer 455454`
    // })


    const user: User = JSON.parse(localStorage.getItem('user'));
    const requestToken = {
      userName: user.userName,
      refreshToken: this.getRefreshToken(),
      grantType: 'RefreshToken'
    }
    return this.http.post<any>(this.baseUrl + 'Auth', requestToken).pipe(
      tap((res: any) => {
        this.storeJwtToken(res.data.accessToken);
      }));
  }


  private storeJwtToken(jwt: string) {
    localStorage.setItem('token', jwt);
  }


  roleMatch(allowedRoles): boolean {

    let isMatch = false;
    const user = JSON.parse(localStorage.getItem('user'));
    const roles = user.roles as Array<string>
    allowedRoles.forEach(element => {
      if (roles.includes(element)) {
        isMatch = true;
      }
    });

    return isMatch;

  }

  getUser(): any {
    return JSON.parse(localStorage.getItem('user'));

  }

  createUser(viewModel: User): Observable<ApiResult> {
    return this.http.post<ApiResult>(this.baseUrl + 'Auth', viewModel)
  }
}
