import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MessageSenderComponent } from './message-sender/message-sender.component';

@Component({
  selector: 'app-root',


  templateUrl: './app.component.html',

})
export class AppComponent {
  title = 'message-frontend';
}