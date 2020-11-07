import { Directive } from '@angular/core';
import { DataBindingDirective, GridComponent } from '@progress/kendo-angular-grid';
import { UserService } from '../../../services/user.service';

@Directive({
  selector: '[appUserBindigDirective]'
})
export class UserBindigDirectiveDirective extends DataBindingDirective {

  constructor(private userService: UserService, grid: GridComponent) {

    super(grid)
  }
  ngOnInit() {

  }
}
