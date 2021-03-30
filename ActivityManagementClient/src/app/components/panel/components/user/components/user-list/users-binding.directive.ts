import { Directive, OnDestroy, OnInit } from '@angular/core';
import { DataBindingDirective, GridComponent } from '@progress/kendo-angular-grid';
import { State } from '@progress/kendo-data-query';
import { Subscription } from 'rxjs';
import { UserGridService } from '../../services/User.Grid.service';

@Directive({
  selector: '[UsersBinding]'
})
export class UsersBindingDirective extends DataBindingDirective implements OnInit, OnDestroy {

  private serviceSubscription: Subscription;
   
  public state: State = {
    skip: 1,
    take: 10,
    // filter: { filters: [], logic: 'or' },
    group: [],
    sort: [],


  }
  constructor(private userGridService: UserGridService, grid: GridComponent) {

    super(grid)
  }

  public ngOnInit() {

    this.serviceSubscription = this.userGridService.subscribe((result) => {
     
      this.grid.loading = false;
      this.grid.data = result;
      this.notifyDataChange();
    });
 
    super.ngOnInit();

    this.rebind();

  }

  public ngOnDestroy(): void {
    if (this.serviceSubscription) {
        this.serviceSubscription.unsubscribe();
    }

    super.ngOnDestroy();
}

  public rebind() {

    this.grid.loading = true;
    this.userGridService.read(this.state)
  }

  addHandler(){
    
  }
}
