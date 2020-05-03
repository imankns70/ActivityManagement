import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http';


import { LayoutComponent } from './Shared/Layout/Layout.component';
import { FirstMessageComponent } from './FirstMessage/FirstMessage.component';
import { RightSideMenuComponent } from './Shared/Right-Side-Menu/Right-Side-Menu.component';
import {UserProfileComponent} from'./Shared/User-Profile/User-Profile.component';
import { from } from 'rxjs';

@NgModule({
   declarations: [
      LayoutComponent,
      FirstMessageComponent,
      RightSideMenuComponent,
      UserProfileComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule
   ],
   providers: [],
   bootstrap: [
      LayoutComponent
   ]
})
export class AppModule { }
