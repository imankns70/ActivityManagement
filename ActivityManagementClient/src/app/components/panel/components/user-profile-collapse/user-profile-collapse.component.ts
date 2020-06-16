import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../auth/services/auth.service';
@Component({
  selector: 'user-profile-collapse',
  templateUrl: './user-profile-collapse.component.html',
  styleUrls: ['./user-profile-collapse.component.css']
})
export class UserProfileCollapseComponent implements OnInit {

  constructor(private router: Router, private authService: AuthService) { }

  ngOnInit() {
  }
  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/auth/login'])
  }
}
