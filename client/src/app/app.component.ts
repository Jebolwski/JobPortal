import { Component } from '@angular/core';
import { AuthenticationService } from './services/authentication.service';
import jwt_decode from 'jwt-decode';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'client';

  constructor(public auth: AuthenticationService) {
    let theme: string | null = localStorage.getItem('theme');
    this.auth.toggleDarkMode(theme);
    if (localStorage.getItem('accessToken')) {
      let user1: any = jwt_decode(localStorage.getItem('accessToken') || '');
      console.log(user1);
      let user_id = user1['id'];
      auth.getUserById(user_id).subscribe();
    }

    
  }
}
