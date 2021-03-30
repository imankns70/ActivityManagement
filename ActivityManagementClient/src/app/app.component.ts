import { Component } from '@angular/core';
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
