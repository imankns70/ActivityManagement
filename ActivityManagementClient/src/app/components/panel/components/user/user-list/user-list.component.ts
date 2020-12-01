import { CreateUserComponent } from '../user-list/create/create-user/create-user.component';
import { Component, OnInit } from '@angular/core';
import { WindowService } from '@progress/kendo-angular-dialog/dist/es2015/window/window.service';
import { DataStateChangeEvent, GridDataResult } from '@progress/kendo-angular-grid';
import { DataSourceRequestState } from '@progress/kendo-data-query';

@Component({
  selector: 'user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {

  public opened = true;
  public dataSaved = false;
  public users: GridDataResult;
  public state: DataSourceRequestState = {
    skip: 0,
    take: 5
  }


  constructor(private windowService: WindowService) { }

  ngOnInit() {
  }
  getUsers(): GridDataResult {

    Object.assign
    return this.users
  }

  public dataStateChange(stateChanges: DataStateChangeEvent): void {
    this.state = stateChanges;

  }

  public showWindow() {
    const windowRef = this.windowService.open({

      title: 'ایجاد کاربر',
      content: CreateUserComponent,
      width: 600
    })

    const createInfo = windowRef.content.instance;
    createInfo.name = 'iman';
    createInfo.age = '5';
  }

}
