import { HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { ApiResult } from '../models/apiresult';

export function setTokenHeader() {
  return new HttpHeaders({
    //'Content-Type': 'application/json',
    'Authorization': 'Bearer ' + localStorage.getItem('token')
  })
}
 