import { Component, OnInit } from '@angular/core';
import { State } from '@progress/kendo-data-query';
import { UserGridService } from '../../services/User.Grid.service';
import { AuthService } from 'src/app/components/auth/services/auth.service';
import { User } from 'src/app/models/user/user';


@Component({
  selector: 'user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  //@ViewChild('container', { read: ViewContainerRef })

  // public containerRef: ViewContainerRef;
  // public opened = true;
  public isActiveForm : boolean;
  public state: State = {
    skip: 1,
    take: 10,
    // filter: { filters: [], logic: 'or' },
    group: [],
    sort: [],


  }

  constructor(private userGridService: UserGridService, private authService: AuthService) {
  }

  ngOnInit() {



  }

  renderUser() {
   
    this.isActiveForm = true;

  }
  saveHandler(user: User) {
   debugger;
      // const user = Object.assign({}, this.useForm.value)
      // this.authService.createUser(user).subscribe(res => {
  
      //   if (res.isSuccess) {
      //     this.alertService.showMessage(res.data, 'عملیات موفقیت آمیز', Globals.successMessage);
  
      //   } else {
      //     this.alertService.showMessage(res.data, 'خطا در عملیات', Globals.errorMessage);
  
      //   }
  
      // }, error => {
      //   this.alertService.showMessage('خطا رخ داده است', error, Globals.errorMessage);
      // });
  
  }
  cancelHandler() {
    this.isActiveForm = false;
  }










  // public editUser({ dataItem }) {

  //   const windowRef = this.sharedService.renderWindow(this.containerRef, EditUserComponent, 'ویرایش کاربر').content.instance
  //   windowRef.user = dataItem;


  // }

}
