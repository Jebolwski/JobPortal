import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { LoggedService } from './services/logged.service';
import { NotLoggedService } from './services/notlogged.service';

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
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
