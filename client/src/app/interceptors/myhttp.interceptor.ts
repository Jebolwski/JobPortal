import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpClient
} from '@angular/common/http';
import { Observable } from 'rxjs';
import jwt_decode from 'jwt-decode';
import { AuthenticationService } from '../services/authentication.service';
import { Response } from '../interfaces/Response';
import { Router } from '@angular/router';

@Injectable()
export class MyHttpInterceptor implements HttpInterceptor {

  constructor(private http: HttpClient,    private router: Router) {}
  private baseApiUrl: string = 'https://localhost:7179/api/';
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    console.log("tetiklendi..");
    let token = localStorage.getItem("accessToken");
    
    if (token){
      let result:any = jwt_decode(token);
      if (new Date(result['exp']*1000)<new Date(Date.now())){
        this.refresh();
      }
    }
    return next.handle(request);
  }

  public async refresh(){
    await this.http
        .post(this.baseApiUrl + 'Authentication/refresh-token', { reftoken: localStorage.getItem('refreshToken') })
        .subscribe((response: any) => {
          let res: Response = response;
          if (res.statusCode === 200) {
            localStorage.setItem('accessToken', res.responseModel.accessToken);
            localStorage.setItem('refreshToken', res.responseModel.refreshToken);
          } else if (res.statusCode === 401) {
            this.logout();
          } else {
            console.log('hata',res.responseModel.refreshToken);
          }
        });
  }
  
  public logout() {
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('accessToken');
    this.router.navigate(['/login']);
  }
}

