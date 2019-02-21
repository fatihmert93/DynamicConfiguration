import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import {routing} from "./app.routing";
import {AuthenticationService} from "./service/auth.service";
import {ReactiveFormsModule, FormsModule} from "@angular/forms";
import {HttpClientModule} from "@angular/common/http";
import { AddConfigurationComponent } from './add-configuration/add-configuration.component';
import { EditConfigurationComponent } from './edit-configuration/edit-configuration.component';
import {ListConfigurationComponent} from "./list-configuration/list-configuration.component";
import {ConfigurationService} from "./service/configuration.service";

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ListConfigurationComponent,
    AddConfigurationComponent,
    EditConfigurationComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    routing,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [AuthenticationService, ConfigurationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
