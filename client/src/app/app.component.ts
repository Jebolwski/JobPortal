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

  constructor(private auth: AuthenticationService) {
    if (localStorage.getItem('accessToken')) {
      let user1: any = jwt_decode(localStorage.getItem('accessToken') || '');
      console.log(user1);
      let user_id =
        user1[
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid'
        ];
      auth.getUserById(user_id).subscribe();
    }

    setInterval(() => {
      auth.refreshToken({ reftoken: localStorage.getItem('refreshToken') });
    }, 52000);
  }
}
