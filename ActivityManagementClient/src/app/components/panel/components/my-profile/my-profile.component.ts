import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User } from 'src/app/models/user';
import { NotificationMessageService } from 'src/app/Services/NotificationMessage.service';
import { ApiResult } from 'src/app/models/apiresult';
import { Console } from 'console';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css']
})
export class MyProfileComponent implements OnInit {

  users: User[]
  constructor(private userService: UserService, alertService: NotificationMessageService) { }

  ngOnInit() {

     this.getUsers();
  }

  getUsers() {
    const result = this.userService.getUsers().subscribe((apiResult:ApiResult) => {
      if(apiResult.isSuccess==true){
       
      }

    });

  }


  // getUser(): User {

  // }
}
