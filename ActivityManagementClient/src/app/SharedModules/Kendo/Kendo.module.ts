import { NgModule } from '@angular/core';
import { GridModule } from '@progress/kendo-angular-grid';
import { RTL } from '@progress/kendo-angular-l10n';

@NgModule({
  imports: [
    GridModule
  ],
  exports: [GridModule],
  providers: [{ provide: RTL, useValue: true }]
})
export class KendoModule { }
