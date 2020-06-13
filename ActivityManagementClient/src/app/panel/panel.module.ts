import { NgModule } from '@angular/core';
import { PanelComponent } from './panel.component';
import { PanelRoutingModule } from './panel-routing.module';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
  imports: [
    PanelRoutingModule
  ],
  declarations: [PanelComponent],

  providers:[]

})
export class PanelModule {

}
