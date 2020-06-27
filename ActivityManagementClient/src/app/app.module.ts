import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { PanelModule } from './components/panel/panel.module';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
   NgxUiLoaderModule, NgxUiLoaderHttpModule, NgxUiLoaderRouterModule,
   NgxUiLoaderConfig, POSITION, SPINNER, PB_DIRECTION
} from 'ngx-ui-loader';
import { AuthGuard } from './guards/auth.guard';
// import { JwtModule } from '@auth0/angular-jwt';
// import { environment } from 'src/environments/environment';
// import { tokenGetter } from 'src/app/Services/customFunction';

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
      BrowserModule,
      AppRoutingModule,
      PanelModule,
      HttpClientModule,
      BrowserAnimationsModule,
      // JwtModule.forRoot({
      //    config: {
      //      tokenGetter: tokenGetter,
      //      whitelistedDomains: [
      //        environment.apiUrl + 'api/v1/UserManager',
      //      ],
      //      blacklistedRoutes: [
      //        environment.apiUrl + 'api/v1/Account/SignIn',
      //        environment.apiUrl + 'api/v1/Account/Register']
      //    }
      //  }),
      NgxUiLoaderModule.forRoot(ngxUiLoaderConfig),
      NgxUiLoaderRouterModule,
      NgxUiLoaderHttpModule.forRoot({ showForeground: true }),
      ToastrModule.forRoot({
         timeOut: 10000,
         positionClass: 'toast-top-left',
         preventDuplicates: true,
         progressBar: true,
         progressAnimation: 'decreasing'
      }),
   ],
   providers: [AuthGuard],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
