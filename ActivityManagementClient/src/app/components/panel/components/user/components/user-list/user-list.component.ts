import { Component, OnInit } from '@angular/core';
import { State } from '@progress/kendo-data-query';
import { UserGridService } from '../../services/User.Grid.service';
import { User } from 'src/app/models/user/user';
import { NotificationMessageService } from 'src/app/Shared/Services/NotificationMessage.service';
import { Globals } from 'src/app/models/enums/Globals';
import { UserService } from 'src/app/components/panel/services/user.service';
import { FormGroup } from '@angular/forms';
import { error } from '@angular/compiler/src/util';
import { debugOutputAstAsTypeScript } from '@angular/compiler';


@Component({
  selector: 'user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {

  public isNew: boolean;
  //public activeDeleteUser = false;
  public userDataItem: User;
  public userDelete: User;

  public state: State = {
    skip: 1,
    take: 10,
    // filter: { filters: [], logic: 'or' },
    group: [],
    sort: [],


  }

  constructor(private userGridService: UserGridService, private userService: UserService,
    private alertService: NotificationMessageService) {
  }

  ngOnInit() {
  }


  renderDeleteUser({ dataItem }) {

    //this.isActiveEditForm = true;
    this.userDelete = dataItem;

  }
  renderEditUser({ dataItem }) {
    //this.isActiveEditForm = true;
    this.userDataItem = dataItem;
    this.isNew = false;
  }

  renderAddUser() {
    this.isNew = true;
    this.userDataItem = {} as User;
  }

  saveHandler(user: User) {
    user.roleId = 2;
    if (user.id == null) {

      this.userService.createUser(user).subscribe(res => {

        if (res.isSuccess) {


          this.alertService.showMessage(res.data, 'عملیات موفقیت آمیز', Globals.successMessage);
          this.userGridService.read(this.state);
        } else {
          this.alertService.showMessage(res.data, 'خطا در عملیات', Globals.errorMessage);

        }

      }, error => {

        this.alertService.showMessage('خطا رخ داده است', error, Globals.errorMessage);
      });
    }
    else {
   
      this.userService.editUser(user).subscribe(res => {

        if (res.isSuccess) {


          this.alertService.showMessage(res.data, 'عملیات موفقیت آمیز', Globals.successMessage);
          this.userGridService.read(this.state);
        } else {
          this.alertService.showMessage(res.data, 'خطا در عملیات', Globals.errorMessage);

        }

      }, error => {

        this.alertService.showMessage('خطا رخ داده است', error, Globals.errorMessage);
      });
    }

  }

  deleteHandler(user: User) {
 
    this.userService.deleteUser(user).subscribe(res => {
      if (res.isSuccess) {

        this.alertService.showMessage(res.data, 'عملیات موفقیت آمیز', Globals.successMessage);
        this.userGridService.read(this.state);

      }
      else {
        this.alertService.showMessage(res.data, 'خطا در عملیات', Globals.errorMessage);

      }

    }, error => {
      this.alertService.showMessage('خطا رخ داده است', error, Globals.errorMessage);


    });

  }

  cancelHandler() {
    debugger;
    this.userDataItem = undefined;

    //this.isActiveEditForm = false;
    //this.isActiveEditForm = false;

  }





}
