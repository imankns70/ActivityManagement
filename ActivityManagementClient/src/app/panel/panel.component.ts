import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth/services/auth.service';

@Component({
  selector: 'app-panel',
  templateUrl: './panel.component.html',
  styleUrls: ['./panel.component.css']
})
export class PanelComponent implements OnInit {

  constructor(private router: Router, private authService: AuthService) {

  }

  ngOnInit() {
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/auth/login'])
  }
   
}
