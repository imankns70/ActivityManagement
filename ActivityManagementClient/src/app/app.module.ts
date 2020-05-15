import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { LoginModule } from './login/login.module';
import { PanelModule } from './Panel/panel.module';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { ActivityRoutes } from './Routes/activityroutes';

@NgModule({
   declarations: [
      AppComponent,
   ],
   imports: [
      LoginModule,
      PanelModule,
      BrowserModule,
      RouterModule.forRoot(ActivityRoutes)

   ],
   providers: [],
   bootstrap: [AppComponent]
})
export class AppModule { }
