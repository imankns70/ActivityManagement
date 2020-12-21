import { CreateUserComponent } from '../user-list/create/create-user/create-user.component';
<<<<<<< HEAD
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
=======
import { Component, OnInit } from '@angular/core';
>>>>>>> f09bac7c1f31fb28c065bfeb44e24ecceec8fbe8
import { WindowService } from '@progress/kendo-angular-dialog';
import { DataStateChangeEvent, GridDataResult } from '@progress/kendo-angular-grid';
import { DataSourceRequestState } from '@progress/kendo-data-query';

@Component({
  selector: 'user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
@ViewChild('container',{read:ViewContainerRef})
public containerRef:ViewContainerRef;
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

      appendTo:this.containerRef,
      title: 'ایجاد کاربر',
      content: CreateUserComponent,
      width: 600
    })

    const createInfo = windowRef.content.instance;
    createInfo.name = 'iman';
    createInfo.age = '5';
  }


 
}
