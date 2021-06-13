import { Component, OnInit, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../../../Shared/Services/auth/services/auth.service';
import { Store } from '@ngrx/store';
import * as fromStore from 'src/app/store'

@Component({
  selector: 'user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  imageUrl$: Observable<string>

  constructor(private generalStore: Store<fromStore.State>, private authService: AuthService) { }

  ngOnInit() {
    this.imageUrl$ = this.generalStore.select(fromStore.getUserLoggedPhotoUrl);
    // this.authService.currentPhotoUrl.subscribe(url => this.imageUrl = url)
    // this.authService.loadUser();
  }
  logout() {
    this.authService.logout();
    this.authService.currentUser = null;
  }

 
}
