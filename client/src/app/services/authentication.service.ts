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
  public user_id!: string;
  private baseApiUrl: string = 'https://localhost:7179/api/';
  constructor(
    private http: HttpClient,
    private jwtHelper: JwtHelperService,
    private externalAuthService: SocialAuthService,
    private router: Router,
  ) {
    this.getUserById(this.user_id).subscribe();

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
        tokenExpires: new Date(user_tmp.exp * 1000),
        tokenCreated: new Date(user_tmp.iat * 1000),
      }).subscribe((res) => {
        localStorage.setItem('accessToken', res.accessToken);
        localStorage.setItem('refreshToken', res.refreshToken);
        let user1: any = jwt_decode(res.accessToken);
        this.user_id = user1['id'];
        this.getUserById(this.user_id).subscribe();
        router.navigate(['/']);
      });
      this.extAuthChangeSub.next(user);
    });
  }

  getUserById(id: string) {
    return this.http
      .get(this.baseApiUrl + 'Authentication/get-user/' + id)
      .pipe(
        map((response: any) => {
          let res: Response = response;
          if (res.statusCode === 200) {
            this.user = res.responseModel;
          } else {
            console.log('hata');
          }
        })
      );
  }

  login(data: any) {
    return this.http
      .post(this.baseApiUrl + 'Authentication/login/' , data)
      .pipe(
        map((response: any) => {
          let res: Response = response;
          if (res.statusCode === 200) {
            localStorage.setItem('accessToken',res.responseModel.accessToken);
            localStorage.setItem('refreshToken',res.responseModel.refreshToken.token);
            this.router.navigate(['/']);
          } else {
            console.log('hata');
          }
        })
      );
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

  public refreshToken(data: any): void {
    this.http
      .post(this.baseApiUrl + 'Authentication/refresh-token', data)
      .subscribe((response: any) => {
        let res: Response = response;
        if (res.statusCode === 200) {
          localStorage.setItem('accessToken', res.responseModel.accessToken);
          localStorage.setItem('refreshToken', res.responseModel.refreshToken);
        } else if (res.statusCode === 401) {
          this.logout();
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

  public register(data: any) {
    return this.http
      .post(this.baseApiUrl + 'Authentication/register', data)
      .pipe(
        map((response: any) => {
          let res: Response = response;
          if (res.statusCode === 200) {
            return res;
          } else {
            alert('HATA');
            return null;
          }
        })
      );
  }

  public resetPassword(data: {token:string,newPassword1:string,newPassword2:string}) {
    return this.http.post(this.baseApiUrl+'Authentication/reset-password',data).pipe(
      map((response: any) => {
        let res: Response = response;
        if (res.statusCode === 200) {
          return res;
        } else {
          return null;
        }
      })
    );
  }

  public resetPasswordMail(data: {email:string}) {
    console.log(data);
    
    return this.http.post(this.baseApiUrl+'Authentication/reset-password-mail',data).pipe(
      map((response: any) => {
        let res: Response = response;
        if (res.statusCode === 200) {
          return res;
        } else {
          return null;
        }
      })
    );
  }
}
