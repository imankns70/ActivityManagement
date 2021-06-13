import { Directive, Input, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { AuthService } from '../Shared/Services/auth/services/auth.service';
import { Store } from '@ngrx/store';
import * as fromStore from 'src/app/store'

@Directive({
  selector: '[appHasRole]'
})
export class HasRoleDirective implements OnInit {
  @Input() appHasRole: string[];
  roles:Array<string>
  isVisible = false;
  constructor(private viewContainerRef: ViewContainerRef, private templateRef: TemplateRef<any>,
    private authService: AuthService,
    private generalStore: Store<fromStore.State>) {
      
      this.generalStore.select(fromStore.getUserLoggedState).subscribe(user => {
        this.roles = user.roles
      });

     }


  ngOnInit() {
 
    const roles =  this.roles
    if (!roles) {
      this.viewContainerRef.clear();
    }

    if (this.authService.roleMatch(this.appHasRole)) {
      if (!this.isVisible) {
        this.isVisible = true;
        this.viewContainerRef.createEmbeddedView(this.templateRef);
      } else {
        this.isVisible = false;
        this.viewContainerRef.clear();

      }

    }
  }
}
