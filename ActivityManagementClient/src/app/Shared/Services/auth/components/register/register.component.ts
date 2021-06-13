import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { NotificationMessageService } from 'src/app/Shared/Services/NotificationMessage.service';
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
      firstname: new FormControl('', Validators.required),
      lastname: new FormControl('', Validators.required),
      username: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(10)]),
      confirmpassword: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(10)]),
      gender: new FormControl(true, Validators.required),
      acceptcondition: new FormControl(false)
    },[this.checkPasswordMatch,this.checkAcceptcondition])
  }
   checkPasswordMatch(g:FormGroup){
    return g.get('password').value === g.get('confirmpassword').value ? null  : {mismath:true}
   }
   checkAcceptcondition(g:FormGroup){

    return g.get('acceptcondition').value === true ? null  : {acceptmismath:true}
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
