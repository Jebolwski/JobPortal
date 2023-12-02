import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { LoggedService } from './services/logged.service';
import { NotLoggedService } from './services/notlogged.service';
import { RegisterComponent } from './components/register/register.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { ResetPasswordMailComponent } from './components/reset-password-mail/reset-password-mail.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
    title: 'Giriş Yap',
    canActivate: [NotLoggedService],
  },
  {
    path: '',
    component: HomeComponent,
    title: 'Ev',
    canActivate: [LoggedService],
  },
  {
    path: 'register',
    component: RegisterComponent,
    title: 'Ev',
    canActivate: [NotLoggedService],
  },
  {
    path: 'reset-password/:token',
    component: ResetPasswordComponent,
    title: 'Şifreyi sıfırla',
    canActivate: [NotLoggedService],
  },
  {
    path: 'reset-password-mail',
    component: ResetPasswordMailComponent,
    title: 'Şifreyi sıfırla mail',
    canActivate: [NotLoggedService],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
