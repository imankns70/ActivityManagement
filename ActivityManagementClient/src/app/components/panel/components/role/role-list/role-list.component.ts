import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/components/auth/services/auth.service';

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.scss']
})
export class RoleListComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit() {
   

    const hhhh = this.authService.refreshToken().subscribe(aaa=> {
      debugger;

    });
  }

}
