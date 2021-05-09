import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
 
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { helloMessageState } from 'src/app/store/helloWorld/helloMessageState';

 
@Component({
  selector: 'app-panel',
  templateUrl: './panel.component.html',
  styleUrls: ['./panel.component.css']
})
export class PanelComponent implements OnInit {
helloMessage$: Observable<string> 

  constructor(private store:Store<helloMessageState>,private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.helloMessage$= this.store.select('helloMessage');
 
 
  }

  
  onPersian(){
    this.store.dispatch({type:'PERSIAN'})
  }
  
  onEnglish(){
    this.store.dispatch({type:'ENGLISH'})

  }
}
