import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule, HttpClient, HTTP_INTERCEPTORS} from '@angular/common/http';
import {ReactiveFormsModule} from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { EventService } from './services/event.service';
import { LoginComponent } from './components/login/login.component';
import { AuthIntercepter } from './Intercepter/auth-intercepter';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [
    {provide:HTTP_INTERCEPTORS,useClass:AuthIntercepter,multi:true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
