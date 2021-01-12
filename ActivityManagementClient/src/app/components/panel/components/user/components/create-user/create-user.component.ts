import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user/user';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.scss']
})
export class CreateUserComponent implements OnInit {

  // @Input() public name: string;

  // @Input() public age: number;
  user: User;
  constructor() { }

  ngOnInit() {

  }


}
