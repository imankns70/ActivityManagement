import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NotificationMessageService } from 'src/app/Services/NotificationMessage.service';
import { Globals } from '../Services/Globals';

@Component({
  selector: 'app-panel',
  templateUrl: './panel.component.html',
  styleUrls: ['./panel.component.css']
})
export class PanelComponent implements OnInit {

  constructor(private router:Router) {
   
   }

  ngOnInit() {
  }
  logout(){
    
    localStorage.removeItem('token');
    this.router.navigate(['/auth/login'])
  }
}
