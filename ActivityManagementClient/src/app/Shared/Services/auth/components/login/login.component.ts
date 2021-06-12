import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Globals } from 'src/app/models/enums/Globals';
import { NotificationMessageService } from 'src/app/Shared/Services/NotificationMessage.service';
import { User } from 'src/app/models/user/user';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};
  returnUrl: any = '';
  user: User;
  constructor(private authService: AuthService, private router: Router,
    private alertService: NotificationMessageService, private route: ActivatedRoute) { }

  ngOnInit() {
    debugger;
    this.model.grantType = 'Password';
   
    this.route.queryParams.subscribe(params => this.returnUrl = params['return'] || '/panel/dashboard')

    if (this.authService.isSignIn()) {
      this.router.navigate([this.returnUrl]);
    }
  }

  login() {
    this.authService.login(this.model)
    .subscribe(next => {
  debugger;
      if (next.data.isSuccess == true) {
       

        
        this.router.navigate([this.returnUrl]);


      } else {
        this.alertService.showMessage(next.data.message, "خطا", Globals.errorMessage)
      }
    })
  }

}
