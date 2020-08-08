import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../auth/services/auth.service';
import { NotificationMessageService } from 'src/app/Services/NotificationMessage.service';
import { Globals } from 'src/app/models/enums/Globals';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};
  registerForm: FormGroup;
  constructor(private authService: AuthService, private alertService: NotificationMessageService) { }

  ngOnInit() {
    this.registerForm = new FormGroup({
      firstname: new FormControl('نام را وارد کنید', Validators.required),
      lastname: new FormControl('نام خانوادگی را وارد کنید', Validators.required),
      username: new FormControl('نام کاربری را وارد کنید', Validators.required),
      email: new FormControl('ایمیل را وارد کنید', [Validators.required, Validators.email]),
      password: new FormControl('کلمه عبور را وارد کنید', [Validators.required, Validators.minLength(4), Validators.maxLength(10)]),
      confirmpassword: new FormControl('تکرار کلمه عبور را وارد کنید', [Validators.required, Validators.minLength(4), Validators.maxLength(10)]),
      gender: new FormControl('جنسیت را انتخاب کنید', Validators.required),
      acceptcondition: new FormControl('تیک قوانین سایت را انتخاب کنید', Validators.required)
    })
  }
  register() {
    this.authService.register(this.model).subscribe(p => {
      if (p.isSuccess == true) {
        this.alertService.showMessage(p.message.join(","), "موفق", Globals.successMessage)

      } else {
        this.alertService.showMessage(p.message.join(","), "خطا", Globals.errorMessage)
      }
    }, error => {
      this.alertService.showMessage(error.message.join(","), "خطا", Globals.errorMessage)

    })
  }


}
