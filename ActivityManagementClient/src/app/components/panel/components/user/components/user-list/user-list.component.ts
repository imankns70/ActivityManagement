import { Component, OnInit } from '@angular/core';
import { State } from '@progress/kendo-data-query';
import { UserGridService } from '../../services/User.Grid.service';
import { User } from 'src/app/models/user/user';
import { NotificationMessageService } from 'src/app/Services/NotificationMessage.service';
import { Globals } from 'src/app/models/enums/Globals';
import { UserService } from 'src/app/components/panel/services/user.service';


@Component({
  selector: 'user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  
  public isActiveForm: boolean;
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

    this.isActiveForm = true;

  }
 
  saveHandler(user: User) {
    debugger;
      
    this.userService.createUser(user).subscribe(res => {
      debugger;
      if (res.isSuccess) {
        this.alertService.showMessage(res.data, 'عملیات موفقیت آمیز', Globals.successMessage);
        this.userGridService.query(this.state);
      } else {
        this.alertService.showMessage(res.data, 'خطا در عملیات', Globals.errorMessage);

      }

    }, error => {
      this.alertService.showMessage('خطا رخ داده است', error, Globals.errorMessage);
    });

  }

  
  cancelHandler() {
    this.isActiveForm = false;
  }





}
