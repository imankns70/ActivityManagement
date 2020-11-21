import { Directive } from '@angular/core';
import { DataBindingDirective, GridComponent } from '@progress/kendo-angular-grid';
import { Subscription } from 'rxjs';
import { UserService } from '../../../services/user.service';

@Directive({
  selector: '[UsersBindig]'
})
export class UserBindigDirective extends DataBindingDirective {
  private servieSubscription: Subscription

  constructor(private userService: UserService, grid: GridComponent) {

    super(grid)
  }
  ngOnInit() {

    this.servieSubscription = this.userService.getUsers().subscribe((result) => {
      this.grid.loading = false;
      debugger;
      this.grid.data = result.data;
      this.notifyDataChange();

    })

    super.ngOnInit();
  }
  public ngOnDestroy() {
    if (this.servieSubscription) {
      this.servieSubscription.unsubscribe();
    }
    super.ngOnDestroy();
  }

  public rebind(): void {
    this.grid.loading = true;
    this.state
    //this.userService.
  }
}
