import { Component, OnInit, ViewChild } from '@angular/core';

import { User } from 'src/app/models/user';
import { NotificationMessageService } from 'src/app/Services/NotificationMessage.service';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Globals } from 'src/app/models/enums/Globals';
import { gender } from 'src/app/models/enums/gender';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css']
})
export class MyProfileComponent implements OnInit {
  // to reset the form we need this
  editForm: FormGroup
  user: User;
  constructor(private userService: UserService, private alertService: NotificationMessageService,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder) { }
  
  ngOnInit() {
  
    this.user = this.getUserLoggedIn();
    console.log(this.user.gender)
    this.updateProfileInformation();
  }
  updateProfileInformation() {

    this.editForm = this.formBuilder.group({
      id: [this.user.id],
      userName: [this.user.userName, [Validators.required, Validators.minLength(2), Validators.maxLength(20)]],
      firstName: [this.user.firstName, Validators.required],
      lastName: [this.user.lastName, Validators.required],
      email: [this.user.email, [Validators.required, Validators.email]],
      phoneNumber: [this.user.phoneNumber, Validators.required],
      persianBirthDate: [this.user.persianBirthDate, Validators.required],
      gender: [this.user.gender == gender.Male ? "Male" : "Female", Validators.required],

    })
  }

  getUserLoggedIn(): User {

    let user: User;
    this.route.data.subscribe(data => {

      let jsonString = JSON.stringify(data.user.data)
      user = <User>JSON.parse(jsonString)

    });
    return user;
  }

  updateMyProfile() {
    this.userService.updateMyProfile(this.user).subscribe(next => {

      if (next.isSuccess) {
        this.alertService.showMessage(next.message.join(','), 'موفق', Globals.successMessage);
        this.editForm.reset(this.user);
      }
    }, error => {
      this.alertService.showMessage(error.message.join(','), 'خطا', Globals.successMessage);

    })
  }



}
