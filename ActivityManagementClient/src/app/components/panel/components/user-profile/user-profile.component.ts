import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../auth/services/auth.service';
 
import { User } from 'src/app/models/user';
@Component({
  selector: 'user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  imageUrl:string

  constructor(private router: Router, private authService: AuthService) { }

  ngOnInit() {
 
    this.authService.currentPhotoUrl.subscribe(url => {
      debugger;
      this.imageUrl = url
    })
  }
  logout() {
   this.authService.logout()
  }
}
