import { HttpClient } from '@angular/common/http';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { State, toDataSourceRequestString } from '@progress/kendo-data-query';
import { BehaviorSubject, Observable, } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ApiResult } from '../../models/apiresult';


export abstract class KednoGridService extends BehaviorSubject<GridDataResult> {

  baseUrl = environment.apiUrl;

  public loading: boolean;
 
  constructor(private http: HttpClient, protected action: string) {
    super(null);

  }


  public read(state: any): void {

    this.fetch(this.action,state)
      .subscribe(x => super.next(x));
  }

  public fetch(action: string, state: any): Observable<GridDataResult> {

    //const queryStr = `${toODataString(state)}&$count=true`;
    const queryStr = `${toDataSourceRequestString(state)}`;
    this.loading = true;

    return this.http
      .get(`${this.baseUrl}${action}?${queryStr}`)
      .pipe(
        map((response: ApiResult): GridDataResult => {

          return {

            data: response.data.data,
            total: response.data.total
          };
        }))
  }
}
