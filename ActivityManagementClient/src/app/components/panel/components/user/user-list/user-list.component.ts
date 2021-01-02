import { CreateUserComponent } from '../user-list/create/create-user/create-user.component';
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { WindowService } from '@progress/kendo-angular-dialog';
import { DataStateChangeEvent, GridDataResult } from '@progress/kendo-angular-grid';
import { DataSourceRequestState } from '@progress/kendo-data-query';
import { UserGridService } from '../../../services/User.Grid.service';
import { Observable } from 'rxjs';

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
  public users: Observable<GridDataResult>;
  public state: DataSourceRequestState = {
    skip: 0,
    take: 5
  }

 
  constructor(private windowService: WindowService, private userGridService:UserGridService) { }

  ngOnInit() {
     this.users= this.userGridService;
    this.userGridService.read(this.state)
  }
  

  public dataStateChange(stateChanges: DataStateChangeEvent): void {
    this.state = stateChanges;
    this.userGridService.read(this.state)

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
