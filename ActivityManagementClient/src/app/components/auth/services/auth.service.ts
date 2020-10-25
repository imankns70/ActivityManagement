import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, BehaviorSubject } from 'rxjs';
import { ApiResult } from 'src/app/models/apiresult';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { map } from 'rxjs/operators';


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
    return this.getToken() == null ? false : true;

  }
  getToken(): string {
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

  getNewRefreshToken(): Observable<any> {

    const user: User = JSON.parse(localStorage.getItem('user'));
    const userName = user.userName;
    const refreshToken = localStorage.getItem('refreshToken');
    const grantType = 'RefreshToken';

    const requestToken = {
      userName: userName,
      refreshToken: refreshToken,
      grantType: grantType
    }
   debugger;
    var ffff= this.http.post<any>(this.baseUrl + 'Auth', requestToken)
    

    return ffff
    // .pipe(
    //   map((res: any) => {
    //     debugger;
    //     if (res.data && res.data.accessToken) {
    //       localStorage.setItem('token', res.data.accessToken);
        

    //     }
    //     return res as any;
    //   })
    // )


  }
}
