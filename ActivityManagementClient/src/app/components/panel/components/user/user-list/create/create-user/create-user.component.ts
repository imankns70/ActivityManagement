import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.scss']
})
export class CreateUserComponent implements OnInit {

  
  @Input() public name: string;

  @Input() public age: number;
  constructor() { }

  ngOnInit() {
   
  }

}
