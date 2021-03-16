import { Component, OnInit } from '@angular/core';
import { State } from '@progress/kendo-data-query';
import { UserGridService } from '../../services/User.Grid.service';
import { User } from 'src/app/models/user/user';
import { NotificationMessageService } from 'src/app/Services/NotificationMessage.service';
import { Globals } from 'src/app/models/enums/Globals';
import { UserService } from 'src/app/components/panel/services/user.service';
import { FormGroup } from '@angular/forms';


@Component({
  selector: 'user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {

  public isActiveEditForm: boolean;
  public isActiveCreateForm: boolean;
  public editUser: User;
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

  renderAddUser() {

    this.isActiveCreateForm = true;

  }



  saveHandler(formbuilder: FormGroup) {
    this.isActiveCreateForm = false;
    const user: User = formbuilder.value
    if (user.id != null) {

      this.userService.createUser(formbuilder.value).subscribe(res => {

        if (res.isSuccess) {

          formbuilder.reset()
          this.alertService.showMessage(res.data, 'عملیات موفقیت آمیز', Globals.successMessage);
          this.userGridService.query(this.state);
        } else {
          this.alertService.showMessage(res.data, 'خطا در عملیات', Globals.errorMessage);

        }

      }, error => {

        this.alertService.showMessage('خطا رخ داده است', error, Globals.errorMessage);
      });
    }
    else {
      console.log(user);
    }

  }

  renderEditUser({dataItem}) {
 
    this.isActiveEditForm = true;
    this.editUser = dataItem;
  }

  cancelHandler() {
    this.isActiveEditForm = false;
    this.isActiveEditForm = false;

  }





}
