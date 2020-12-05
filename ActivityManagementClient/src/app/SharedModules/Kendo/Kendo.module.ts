import { NgModule } from '@angular/core';
import { GridModule } from '@progress/kendo-angular-grid';
import { RTL } from '@progress/kendo-angular-l10n';
import { DialogsModule, WindowModule } from '@progress/kendo-angular-dialog';
import { ButtonsModule } from '@progress/kendo-angular-buttons';

@NgModule({
  imports: [
    GridModule,
    ButtonsModule,
    DialogsModule,
    WindowModule
  ],
  exports: [
    GridModule,
    ButtonsModule,
    DialogsModule,
    WindowModule
  ],
  providers: [{ provide: RTL, useValue: true }]
})
export class KendoModule { }
