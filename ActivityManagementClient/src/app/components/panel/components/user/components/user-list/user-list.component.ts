import { CreateUserComponent } from '../create-user/create-user.component';
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { WindowService } from '@progress/kendo-angular-dialog';
import { SharedService } from 'src/app/Services/shared-service';
import { EditUserComponent } from '../edit-user/edit-user.component';


@Component({
  selector: 'user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  @ViewChild('container', { read: ViewContainerRef })

  public containerRef: ViewContainerRef;
  public opened = true;

  constructor(private sharedService: SharedService) {

  }

  ngOnInit() {

  }

  public editUser({ dataItem }) {
 
    const windowRef = this.sharedService.renderWindow(this.containerRef, EditUserComponent, 'ویرایش کاربر').content.instance
    windowRef.user = dataItem;


  }

    public addUser() {
     
       this.sharedService.renderWindow(this.containerRef, CreateUserComponent, 'ایجاد کاربر')


  }

}
