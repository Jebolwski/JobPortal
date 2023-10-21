import { Injectable } from '@angular/core';
import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { GoogleLoginProvider } from '@abacritt/angularx-social-login';
import { Subject, map } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Response } from '../interfaces/Response';
import jwt_decode from 'jwt-decode';
import { User } from '../interfaces/User';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private authChangeSub = new Subject<boolean>();
  private extAuthChangeSub = new Subject<SocialUser>();
  public authChanged = this.authChangeSub.asObservable();
  public extAuthChanged = this.extAuthChangeSub.asObservable();
  public user!: User;
  private baseApiUrl: string = 'https://localhost:7179/api/';
  constructor(
    private http: HttpClient,
    private jwtHelper: JwtHelperService,
    private externalAuthService: SocialAuthService,
    private router: Router
  ) {
    this.externalAuthService.authState.subscribe((user) => {
      let user_tmp: any = jwt_decode(user.idToken);
      this.AddUser({
        name: user.name,
        email: user.email,
        firstName: user.firstName,
        lastName: user.lastName,
        photoUrl: user.photoUrl,
        googleUserId: user.id,
        refreshToken: user.idToken,
        tokenExpires: user_tmp.exp,
        tokenCreated: user_tmp.iat,
      }).subscribe((res) => {
        localStorage.setItem('accessToken', res.accessToken);
        localStorage.setItem('refreshToken', res.accessToken);
        this.user = jwt_decode(res.accessToken);
        router.navigate(['/']);
      });
      this.extAuthChangeSub.next(user);
    });
  }

  AddUser(data: any) {
    return this.http
      .post(this.baseApiUrl + 'Authentication/add-user', data, {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .pipe(
        map((response: any) => {
          let res: Response = response;
          if (res.statusCode === 200) {
            return res.responseModel;
          } else {
            alert('HATA');
          }
        })
      );
  }

  public refreshToken(data: any) {
    return this.http
      .post(this.baseApiUrl + 'Authentication/refresh-token', data)
      .subscribe((response: any) => {
        let res: Response = response;
        if (res.statusCode === 200) {
          console.log(res);
        } else {
          console.log('hata');
        }
      });
  }

  public signInWithGoogle = () => {
    this.externalAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
  };

  public signOutExternal = () => {
    this.externalAuthService.signOut();
  };

  public logout() {
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('accessToken');
    this.router.navigate(['/login']);
  }
}
