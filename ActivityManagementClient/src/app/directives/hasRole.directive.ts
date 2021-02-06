import { Directive, Input, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { AuthService } from '../components/auth/services/auth.service';

@Directive({
  selector: '[appHasRole]'
})
export class HasRoleDirective implements OnInit {
  @Input() appHasRole: string[];
  isVisible = false;
  constructor(private viewContainerRef: ViewContainerRef, private templateRef: TemplateRef<any>,
    private authService: AuthService) { }


  ngOnInit() {
 
    const roles = this.authService.getUser().roles as Array<string>;
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
