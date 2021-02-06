import { Component, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { User } from 'src/app/models/user/user';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.scss']
})
export class EditUserComponent implements OnInit {

@Input() user:User;  
 
  constructor() { }

  ngOnInit() {
  }

}
