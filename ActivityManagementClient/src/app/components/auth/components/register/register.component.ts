import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { NotificationMessageService } from 'src/app/Services/NotificationMessage.service';
import { Globals } from 'src/app/Services/Globals';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};
  constructor(private authService: AuthService, private alertService: NotificationMessageService,
    private globals: Globals) { }

  ngOnInit() {
  }
  register() {
    this.authService.register(this.model).subscribe(p => {
      if (p.isSuccess == true) {
        this.alertService.showMessage(p.message, "موفق", this.globals.successMessage)

      } else {
        this.alertService.showMessage(p.message, "خطا", this.globals.errorMessage)
      }
    }, error => {
      this.alertService.showMessage(error.message, "خطا", this.globals.errorMessage)

    })
  }
}
