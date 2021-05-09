import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { TitleService } from './Shared/Services/titleservice.service';
 
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(private titleService :TitleService) {
       
  }
ngOnInit(){

  this.titleService.init()
}


}
