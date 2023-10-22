import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './authentication.service';
import jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class NotLoggedService {
  constructor(private auth: AuthenticationService) {}

  canActivate(): boolean {
    let flag: boolean = false;
    if (
      !(
        localStorage.getItem('accessToken') != null &&
        jwt_decode(localStorage.getItem('accessToken') || '') != null
      )
    ) {
      flag = true;
    }
    return flag;
  }
}
