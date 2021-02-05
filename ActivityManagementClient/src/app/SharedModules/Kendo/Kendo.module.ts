import { NgModule } from '@angular/core';
import { GridModule } from '@progress/kendo-angular-grid';
import { RTL } from '@progress/kendo-angular-l10n';
import { DialogModule, DialogsModule } from '@progress/kendo-angular-dialog';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';

@NgModule({
  imports: [
    GridModule,
    ButtonsModule,
    DialogsModule,
    DialogModule,
    InputsModule,
    LabelModule
  ],
  exports: [
    GridModule,
    ButtonsModule,
    DialogsModule,
    DialogModule,
    InputsModule,
    LabelModule
  ],
  providers: [{ provide: RTL, useValue: true }]
})
export class KendoModule { }
