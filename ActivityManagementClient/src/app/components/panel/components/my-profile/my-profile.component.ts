import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User } from 'src/app/models/user';
import { NotificationMessageService } from 'src/app/Services/NotificationMessage.service';
import { ApiResult } from 'src/app/models/apiresult';
import { Console } from 'console';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css']
})
export class MyProfileComponent implements OnInit {

   user: User = new User();
  constructor(private userService: UserService, alertService: NotificationMessageService, private route: ActivatedRoute) { }

  ngOnInit() {

    this.getUserLoggedIn();
  }

  getUserLoggedIn(): User {
    
    this.route.data.subscribe(data => {

      this.user.id = data.user.data.id;
      this.user.firstName = data.user.data.firstName;
      this.user.lastName = data.user.data.lastName;
      this.user.birthDate = data.user.data.birthDate;
      this.user.email = data.user.data.email;
      this.user.phoneNumber = data.user.data.phoneNumber;
      this.user.userName = data.user.data.userName;
      this.user.gender = data.user.data.gender;

    });
    return this.user;
  }


  // getUser(): User {

  // }
}
