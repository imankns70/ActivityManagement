import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Globals } from 'src/app/models/enums/Globals';
import { NotificationMessageService } from 'src/app/Services/NotificationMessage.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};
  returnUrl: any = '';
  constructor(private authService: AuthService, private router: Router,
    private alertService: NotificationMessageService, private route: ActivatedRoute) { }

  ngOnInit() {
  
    this.route.queryParams.subscribe(params => this.returnUrl = params['return'] || '/panel/dashboard')
    
    if (this.authService.isSignIn()) {
      this.router.navigate([this.returnUrl]);
    }
  }
  login() {
    this.authService.login(this.model).subscribe(p => {

      if (p.isSuccess == true) {

        this.router.navigate([this.returnUrl]);

        // this.alertService.showMessage(p.message, "موفق", this.globals.successMessage)

      } else {
        this.alertService.showMessage(p.message.join(","), "خطا", Globals.errorMessage)
      }
    }, error => {
      this.alertService.showMessage(error.message.join(","), "خطا", Globals.errorMessage)

    })
  }

}
