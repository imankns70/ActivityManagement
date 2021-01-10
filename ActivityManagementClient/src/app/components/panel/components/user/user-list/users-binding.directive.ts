import { Directive, OnDestroy, OnInit } from '@angular/core';
import { DataBindingDirective, GridComponent } from '@progress/kendo-angular-grid';
import { Subscription } from 'rxjs';
import { UserGridService } from '../services/User.Grid.service';

@Directive({
  selector: '[UsersBinding]'
})
export class UsersBindingDirective extends DataBindingDirective implements OnInit, OnDestroy {

  private serviceSubscription: Subscription;
  
  constructor(private userGridService: UserGridService, grid: GridComponent) {

    super(grid)
  }

 public ngOnInit() {
 
    this.serviceSubscription = this.userGridService.subscribe((result) => {
  
      this.grid.loading = false;
      this.grid.data = result;
      this.notifyDataChange();
    });
    debugger;
    super.ngOnInit();

    this.rebind();

  }

  public ngOnDestroy() {

    this.serviceSubscription.unsubscribe()

  }

 public rebind() {
   debugger;
    this.grid.loading = true;
    this.userGridService.query(this.state)
  }
}
