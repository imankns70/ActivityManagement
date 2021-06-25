import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { TitleService } from './Shared/Services/titleservice.service';
import * as fromStore from '../app/store'
import { AuthService } from './Shared/Services/auth/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(private titleService: TitleService,
    private authService: AuthService,
    private generalStore: Store<fromStore.State>) {

  }
  ngOnInit() {

    this.titleService.init();
   
    if (this.authService.isSignIn()) {

      if (this.authService.currentUser.id === 0) {
        this.generalStore.dispatch(new fromStore.LoadLoggedUser())
      }
    }
  }


}
