import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AuthModule } from './auth/auth.module';
import { PanelModule } from './Panel/panel.module';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { ActivityRoutes } from './Routes/activityroutes';

@NgModule({
   declarations: [
      AppComponent
   ],
   imports: [
      AuthModule,
      PanelModule,
      BrowserModule,
      RouterModule.forRoot(ActivityRoutes)
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
