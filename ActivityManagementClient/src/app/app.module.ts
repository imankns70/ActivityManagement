import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http';


import { AppComponent } from './app.component';
import { FirstMessageComponent } from './FirstMessage/FirstMessage.component';

@NgModule({
   declarations: [
      AppComponent,
      FirstMessageComponent
   ],
   imports: [
      BrowserModule, HttpClientModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
