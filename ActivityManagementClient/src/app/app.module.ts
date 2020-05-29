import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AuthModule } from './auth/auth.module';
import { PanelModule } from './panel/panel.module';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { ToastrModule } from 'ngx-toastr';
import { Globals } from '../app/Services/Globals'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxUiLoaderModule, NgxUiLoaderHttpModule, NgxUiLoaderRouterModule,
    NgxUiLoaderConfig, POSITION, SPINNER, PB_DIRECTION } from 'ngx-ui-loader';
import { AuthGuard } from './guards/auth.guard';

const ngxUiLoaderConfig: NgxUiLoaderConfig = {
   pbColor:'red',

   bgsColor: 'red',
   bgsPosition: POSITION.bottomRight,
   bgsSize: 70,

   fgsPosition:POSITION.bottomRight,
   fgsSize:70,
   fgsColor:'red',


   bgsType: SPINNER.circle, // background spinner type
   fgsType: SPINNER.circle, // foreground spinner type
   pbDirection: PB_DIRECTION.leftToRight, // progress bar direction
   pbThickness: 4 // progress bar thickness
 };
 
@NgModule({
   declarations: [
      AppComponent
   ],
   imports: [
      AuthModule,
      PanelModule,
      BrowserModule,
      HttpClientModule,
      AppRoutingModule,      
      BrowserAnimationsModule,
      NgxUiLoaderModule.forRoot(ngxUiLoaderConfig),
      NgxUiLoaderRouterModule,
      NgxUiLoaderHttpModule.forRoot({showForeground:true}),
      ToastrModule.forRoot({
         timeOut: 10000,
         positionClass: 'toast-top-left',
         preventDuplicates: true,
         progressBar: true,
         progressAnimation: 'decreasing'
      }),
   ],
   providers: [Globals,AuthGuard],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
