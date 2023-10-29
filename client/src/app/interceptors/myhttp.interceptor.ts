import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import jwt_decode from 'jwt-decode';
import { AuthenticationService } from '../services/authentication.service';

@Injectable()
export class MyHttpInterceptor implements HttpInterceptor {

  constructor(private auth:AuthenticationService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    console.log("tetiklendi..");
    let token = localStorage.getItem("accessToken");
    if (token){
      let result:any = jwt_decode(token);
      
      if (new Date(result['exp']*1000)<new Date(Date.now())){
        console.log("tarihi geçmiş");
        this.auth.refreshToken({ reftoken: localStorage.getItem('refreshToken') });
      }
    }
    return next.handle(request);
  }
}

