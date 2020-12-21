import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult } from 'src/app/models/apiresult';
import { User } from 'src/app/models/user/user';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { State, toODataString, translateDataSourceResultGroups } from '@progress/kendo-data-query';
import { map, tap } from 'rxjs/operators';



@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl + 'UserManager/';
  public loading: boolean;
  constructor(private http: HttpClient) { }

  // getUsers(): Observable<any> {

  //   return this.http.get<any>(this.baseUrl + 'GetUsers')


  // }

  GetUserLoggedIn(): Observable<ApiResult> {
    return this.http.get<ApiResult>(this.baseUrl + 'GetUserLoggedIn')

  }

  updateMyProfile(viewModel: User): Observable<ApiResult> {
    return this.http.post<ApiResult>(this.baseUrl + 'UpdateUserProfile', viewModel)
  }

  public getUsers(state: State): Observable<GridDataResult> {
    const queryStr = `${toODataString(state)}&count=true`;
    this.loading = true

    return this.http.get(this.baseUrl + 'GetUsers?' + queryStr)
      .pipe(

        map(({ data, total }: GridDataResult): GridDataResult => {

          debugger;
          return {

            data: data,
            total: total
          };
        }
        ));

  }
}
