import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { State, toODataString, translateDataSourceResultGroups } from '@progress/kendo-data-query';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { ApiResult } from 'src/app/models/apiresult';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class UserGridService extends BehaviorSubject<GridDataResult> {
  baseUrl = environment.apiUrl + 'UserManager/';

constructor(private http: HttpClient) {
  super(null);
}

public read(state: State){
  this.fetch(state)
  .subscribe(x=> super.next(x))
}



public fetch(state: State): Observable<GridDataResult> {
  const queryStr = `${toODataString(state)}&count=true`;

  return this.http.get(this.baseUrl + 'GetUsers?' + queryStr)
    .pipe(

      map((response:ApiResult): GridDataResult => {

        debugger
        return {
          
          data: response.data.data,
          total: response.data.total
        };
      }
      ));

}
}
 
