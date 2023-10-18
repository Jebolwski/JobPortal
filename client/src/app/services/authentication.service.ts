import { Injectable } from '@angular/core';
import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { GoogleLoginProvider } from '@abacritt/angularx-social-login';
import { Subject, map } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Response } from '../interfaces/Response';
@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private authChangeSub = new Subject<boolean>();
  private extAuthChangeSub = new Subject<SocialUser>();
  public authChanged = this.authChangeSub.asObservable();
  public extAuthChanged = this.extAuthChangeSub.asObservable();
  private baseApiUrl: string = 'https://localhost:7179/api/';
  constructor(
    private http: HttpClient,
    private jwtHelper: JwtHelperService,
    private externalAuthService: SocialAuthService
  ) {
    this.externalAuthService.authState.subscribe((user) => {
      console.log(user);
      this.AddUser({
        name: user.name,
        email: user.email,
        firstName: user.firstName,
        lastName: user.lastName,
        photoUrl: user.photoUrl,
        googleUserId: user.id,
      }).subscribe();
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
            console.log('grazie');

            return res.responseModel;
          } else {
            alert('HATA');
          }
        })
      );
  }

  public signInWithGoogle = () => {
    this.externalAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
  };
  public signOutExternal = () => {
    this.externalAuthService.signOut();
  };
}
