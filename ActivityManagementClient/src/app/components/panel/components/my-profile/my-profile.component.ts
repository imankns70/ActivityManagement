import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User } from 'src/app/models/user';
import { NotificationMessageService } from 'src/app/Services/NotificationMessage.service';
import { ApiResult } from 'src/app/models/apiresult';
import { gender } from '../../../../models/enums/gender';
import { ActivatedRoute } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css']
})
export class MyProfileComponent implements OnInit {
  // to reset the form we need this
  @ViewChild('editForm', { static: false }) editForm: NgForm;
  user: User
  constructor(private userService: UserService, alertService: NotificationMessageService, private route: ActivatedRoute) { }

  ngOnInit() {

    this.user = this.getUserLoggedIn();
     
  }

  getUserLoggedIn(): User {
  
    let user: User = new User()
    this.route.data.subscribe(data => {



      user.id = data.user.data.id;
      user.firstName = data.user.data.firstName;
      user.lastName = data.user.data.lastName;
      user.birthDate = data.user.data.persianBirthDate;
      user.email = data.user.data.email;
      user.phoneNumber = data.user.data.phoneNumber;
      user.userName = data.user.data.userName;
      user.gender = data.user.data.gender != undefined ? (data.user.data.gender == 1 ? gender.men : gender.women) : null;

    });
    return user;
  }

  updateMyProfile() {
    this.editForm.reset(this.user);
  }


}
