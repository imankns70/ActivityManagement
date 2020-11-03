import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
//import { PanelModule } from './components/panel/panel.module';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
   NgxUiLoaderModule, NgxUiLoaderHttpModule, NgxUiLoaderRouterModule,
   NgxUiLoaderConfig, POSITION, SPINNER, PB_DIRECTION
} from 'ngx-ui-loader';
import { AuthService } from '../app/components/auth/services/auth.service';
//import { ErrorInterceptorPrivider } from './Services/http-error.interceptor';
import { AuthInterceptor } from './Services/AuthInterceptor.interceptor';
import { GridModule } from '@progress/kendo-angular-grid';



const ngxUiLoaderConfig: NgxUiLoaderConfig = {
   pbColor: 'red',

   bgsColor: 'red',
   bgsPosition: POSITION.bottomRight,
   bgsSize: 70,

   fgsPosition: POSITION.bottomRight,
   fgsSize: 70,
   fgsColor: 'red',


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
      AppRoutingModule,
      BrowserModule,
      //PanelModule,
      HttpClientModule,
      BrowserAnimationsModule,

      NgxUiLoaderModule.forRoot(ngxUiLoaderConfig),
      NgxUiLoaderRouterModule,
      NgxUiLoaderHttpModule.forRoot({ showForeground: true }),
      ToastrModule.forRoot({
         timeOut: 10000,
         positionClass: 'toast-top-left',
         preventDuplicates: true,
         progressBar: true,
         progressAnimation: 'decreasing',

      }),
      GridModule,
   ],
   providers: [
      {
         provide: HTTP_INTERCEPTORS,
         useClass: AuthInterceptor,
         multi: true
      },
      AuthService],

   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
