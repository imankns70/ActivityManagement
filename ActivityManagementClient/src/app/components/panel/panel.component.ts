import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../Shared/Services/auth/services/auth.service';
import { User } from 'src/app/models/user/user';

@Component({
  selector: 'app-panel',
  templateUrl: './panel.component.html',
  styleUrls: ['./panel.component.css']
})
export class PanelComponent implements OnInit {
  constructor(private route: ActivatedRoute) {

  }

  ngOnInit() {
  }

  

}
