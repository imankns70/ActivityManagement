import { HttpHeaders } from '@angular/common/http';

export function setTokenHeader() {
  return new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'Bearer ' + localStorage.getItem('token')
  })
}