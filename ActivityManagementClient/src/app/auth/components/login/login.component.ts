import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { Globals } from 'src/app/Services/Globals';
import { NotificationMessageService } from 'src/app/Services/NotificationMessage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};
  constructor(private authService: AuthService, private router: Router,
    private alertService: NotificationMessageService,
    private globals: Globals) { }

  ngOnInit() {
  }
  login() {
    
    this.authService.login(this.model).subscribe(p => {

      if (p.isSuccess == true) {
        this.router.navigate(['/panel']);

        this.alertService.showMessage(p.message, "موفق", this.globals.successMessage)

      } else {
        this.alertService.showMessage(p.message, "خطا", this.globals.errorMessage)
      }
    }, error => {
      this.alertService.showMessage(error.message, "خطا", this.globals.errorMessage)

    })
  }

}
