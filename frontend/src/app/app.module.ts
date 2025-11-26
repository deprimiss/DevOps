import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { MessageSenderComponent } from './message-sender/message-sender.component';

@NgModule({
  declarations: [
    MessageSenderComponent,
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule, // ← Это регистрирует HttpClient
    FormsModule       // ← Это для ngModel
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
