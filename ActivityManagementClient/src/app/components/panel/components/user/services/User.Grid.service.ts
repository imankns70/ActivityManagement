import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { KednoGridService } from 'src/app/Services/kedno-grid.service';


@Injectable()
export class UserGridService extends KednoGridService {

  constructor(http: HttpClient) {
  
    super(http, "UserManager/GetUsers")

  }

  public queryForUserName(userName: string, state: any) {

    this.query(Object.assign({}, state, {
      filter: {
        filters: [{
          filed: 'username', operator: 'contains', value: userName
        }],
        logic: 'and'
      }

    }))
  }



}

