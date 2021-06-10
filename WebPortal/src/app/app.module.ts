import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER, LOCALE_ID } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { LayoutModule } from '@angular/cdk/layout';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialUiModule } from './core/material-ui/material-ui.module';
import { NavBarComponent } from './core/nav-bar/nav-bar.component';
import { SharedModule } from './core/shared/shared.module';
import { MAT_SNACK_BAR_DEFAULT_OPTIONS } from '@angular/material/snack-bar';
import { PageLinkComponent } from './core/page-link/page-link.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { TimeManagementComponent } from './components/time-management/time-management.component';
import {NgxMaterialTimepickerModule} from 'ngx-material-timepicker';
import { TimeManagementAddComponent } from './components/time-management/dialogs/time-management-add/time-management-add.component';
import { SettingsProvider } from "./services/settingsprovider";

export function initializeApp(appConfig: SettingsProvider) {
  return () => appConfig.load();
}

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    PageLinkComponent,
    DashboardComponent,
    TimeManagementComponent,
    TimeManagementAddComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MaterialUiModule,
    BrowserAnimationsModule,
    LayoutModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    NgxMaterialTimepickerModule,
  ],
  providers: [
    SettingsProvider,{
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [SettingsProvider],
      multi: true,
    },
    { provide: LOCALE_ID, useValue: 'en-GB' },
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    TimeManagementAddComponent,
  ],
})
export class AppModule {}
