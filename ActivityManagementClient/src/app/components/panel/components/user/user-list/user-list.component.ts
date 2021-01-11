import { CreateUserComponent } from '../user-list/create/create-user/create-user.component';
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { WindowService } from '@progress/kendo-angular-dialog';
import { GridDataResult } from '@progress/kendo-angular-grid';
 

@Component({
  selector: 'user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  @ViewChild('container', { read: ViewContainerRef })

  public containerRef: ViewContainerRef;
  public opened = true;
  public dataSaved = false;
  public users: GridDataResult;



  constructor(private windowService: WindowService) {

    

  }

  ngOnInit() {
   

  }

 public editHandler({dataItem}){
 
 console.log(dataItem);
 }

  public showWindow() {
    const windowRef = this.windowService.open({

      appendTo: this.containerRef,
      title: 'ایجاد کاربر',
      content: CreateUserComponent,
      width: 600
    })

    const createInfo = windowRef.content.instance;
    createInfo.name = 'iman';
    createInfo.age = '5';
  }



}
