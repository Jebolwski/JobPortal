import { Component } from '@angular/core';
import { AuthenticationService } from './services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'client';

  constructor(private auth: AuthenticationService) {
    setInterval(() => {
      auth.refreshToken({ reftoken: localStorage.getItem('refreshToken') });
    }, 16000);
  }
}
