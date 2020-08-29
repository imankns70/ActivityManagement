import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User } from 'src/app/models/user';
import { NotificationMessageService } from 'src/app/Services/NotificationMessage.service';
import { ApiResult } from 'src/app/models/apiresult';
import { gender } from '../../../../models/enums/gender';
import { ActivatedRoute } from '@angular/router';
import { NgForm, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Globals } from 'src/app/models/enums/Globals';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css']
})
export class MyProfileComponent implements OnInit {
  // to reset the form we need this
  editForm:FormGroup
  user: User;
  constructor(private userService: UserService, private alertService: NotificationMessageService, private route: ActivatedRoute,
    private formBuilder:FormBuilder) { }

  ngOnInit() {

    this.user = this.getUserLoggedIn();
  
  }
  updateProfileInformation(){
    this.editForm= this.formBuilder.group({
      id:[''],
      username:['',Validators.required],
      firstname:['',Validators.required],
      lastname:['',Validators.required],
      email:['',Validators.required],
      phonenumber:['',Validators.required],
      persianBirthDate:['',Validators.required],
      gender:['',Validators.required],

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
