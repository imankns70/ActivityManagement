import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, BehaviorSubject } from 'rxjs';
import { ApiResult } from 'src/app/models/apiresult';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'Account/';
  currentUser:User;
  imageUrl= new BehaviorSubject<string>('../../../../assets/images/UserPic.png');
  currentPhotoUrl= this.imageUrl.asObservable();
  constructor(private router:Router,private http: HttpClient) { }
  
  login(viewModel: any): Observable<ApiResult> {


    return this.http.post<ApiResult>(this.baseUrl + 'SignIn', viewModel)

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
    this.router.navigate(['/auth/login'])
  }
  changeUserPhoto(url:string){
 
    this.imageUrl.next(url);
  }
}
