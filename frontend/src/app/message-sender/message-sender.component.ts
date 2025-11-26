import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-message-sender',
  templateUrl: './message-sender.component.html',
})
export class MessageSenderComponent {
  messageText: string = '';
  responseMessage: string = '';
  errorMessage: string = '';

    // Новые поля для второго типа запроса
    additionalInput: string = '';
    additionalResponse: string = '';
    additionalError: string = '';
  private apiUrl = '/api/messages';
  constructor(private http: HttpClient) {}
  sendMessage(): void {
    if (!this.messageText.trim()) {
      return;
    }
    this.errorMessage = '';
    this.responseMessage = '';
    const messageData = {
      text: this.messageText
    };
    this.http.post<any>(this.apiUrl, messageData)
      .subscribe({
        next: (response) => {
          this.responseMessage = response.message;
          this.messageText = '';
        },
        error: (error) => {
          this.errorMessage = error.message;
          console.error('Ошибка:', error);
        }
      });
  }

  fetchData(): void {
    this.additionalError = '';
    this.additionalResponse = '';
    this.http.get<any>(this.apiUrl)
      .subscribe({
        next: (response) => {
          if (response) {
            this.additionalResponse = response.message;
            console.log('Полученные сообщения:', response);
          }
        },
        error: (error) => {
          this.additionalError = error.error?.error || error.message || 'Ошибка при получении данных';
          console.error('Ошибка получения:', error);
        }
      });
  }
}