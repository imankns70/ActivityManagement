import { Component, OnInit } from '@angular/core';

import { Observable } from 'rxjs';

 
@Component({
  selector: 'app-panel',
  templateUrl: './panel.component.html',
  styleUrls: ['./panel.component.css']
})
export class PanelComponent implements OnInit {
helloMessage$: Observable<string> 

  constructor() {

  }

  ngOnInit() {
 
 
  }

 
}
