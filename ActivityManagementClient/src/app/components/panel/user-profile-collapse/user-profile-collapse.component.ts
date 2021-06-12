import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../Shared/Services/auth/services/auth.service';
import { Store } from '@ngrx/store';
import * as fromStore from 'src/app/store'
import { Observable } from 'rxjs';
@Component({
  selector: 'user-profile-collapse',
  templateUrl: './user-profile-collapse.component.html',
  styleUrls: ['./user-profile-collapse.component.css']
})
export class UserProfileCollapseComponent implements OnInit {
  imageUrl$: Observable<string>
  constructor(private generalStore: Store<fromStore.State>,
    private authService: AuthService) { }

  ngOnInit() {


    this.imageUrl$ = this.generalStore.select(fromStore.getUserLoggedPhotoUrl);
    // this.authService.currentPhotoUrl.subscribe(url => this.imageUrl = url)
    // this.authService.loadUser();
  }
  logout() {
    this.authService.logout()
  }
}
