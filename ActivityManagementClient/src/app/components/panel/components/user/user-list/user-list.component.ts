import { Component, OnInit } from '@angular/core';
import { DataStateChangeEvent, GridDataResult } from '@progress/kendo-angular-grid';
import { DataSourceRequestState } from '@progress/kendo-data-query';

@Component({
  selector: 'user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {

  
  public users: GridDataResult;
  public state: DataSourceRequestState = {
    skip: 0,
    take: 5
  }


  constructor() { }

  ngOnInit() {
  }
  getUsers(): GridDataResult {

    Object.assign
    return this.users
  }

  public dataStateChange(stateChanges: DataStateChangeEvent): void {
    this.state = stateChanges;

  }

}
