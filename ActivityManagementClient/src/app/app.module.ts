import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AuthModule } from './auth/auth.module';
import { PanelModule } from './Panel/panel.module';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { ActivityRoutes } from './Routes/activityroutes';
import { ToastrModule } from 'ngx-toastr';
import { Globals } from '../app/Services/Globals'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoadingBarHttpClientModule } from '@ngx-loading-bar/http-client';
import { LoadingBarRouterModule } from '@ngx-loading-bar/router';
import { LoadingBarModule } from '@ngx-loading-bar/core';


@NgModule({
   declarations: [
      AppComponent
   ],
   imports: [
      AuthModule,
      PanelModule,
      BrowserModule,
      HttpClientModule,
      RouterModule.forRoot(ActivityRoutes),
      LoadingBarHttpClientModule,
      LoadingBarRouterModule,
     
      BrowserAnimationsModule,
      ToastrModule.forRoot({
         timeOut: 10000,
         positionClass: 'toast-top-left',
         preventDuplicates: true,
         progressBar: true,
         progressAnimation: 'decreasing'
      }),
   ],
   providers: [Globals],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
