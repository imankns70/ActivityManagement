import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../auth/services/auth.service';
import { SharedService } from 'src/app/Services/Shared.service';
import { User } from 'src/app/models/user';
@Component({
  selector: 'user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  user = new User();
  constructor(private router: Router, private authService: AuthService,
    private sharedService: SharedService) { }

  ngOnInit() {

    this.sharedService.imageUrl.subscribe(url => {
      debugger;
      this.user.Image = url
    })
  }
  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/auth/login'])
  }
}
