import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './authentication.service';
import jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class LoggedService {
  constructor(private auth: AuthenticationService, private router: Router) {}

  canActivate(): boolean {
    let flag: boolean = false;
    if (
      localStorage.getItem('accessToken') != null &&
      jwt_decode(localStorage.getItem('accessToken') || '') != null
    ) {
      this.auth.user = jwt_decode(localStorage.getItem('accessToken') || '');
      flag = true;
    } else {
      this.auth.logout();
      this.router.navigate(['/login']);
    }
    return flag;
  }
}
