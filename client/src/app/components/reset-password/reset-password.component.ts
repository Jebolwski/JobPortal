import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import jwt_decode from 'jwt-decode';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent {
  token!:string;
  
  constructor(private router: Router,private authService:AuthenticationService,private route: ActivatedRoute){
    this.token = this.route.snapshot.paramMap.get('token') || '0'
    let token_data: {id:string, exp:number} = jwt_decode(this.token);
    if (new Date(token_data['exp']*1000)<new Date(Date.now())){
      this.router.navigate(['/login']);
    }
  }

  public resetPasswordForm: FormGroup = new FormGroup({
    newPassword1: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
      Validators.maxLength(15),
    ]),
    newPassword2: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
      Validators.maxLength(15),
    ]),
  });

  get newPassword1(){
    return this.resetPasswordForm.get('newPassword1');
  }
  get newPassword2(){
    return this.resetPasswordForm.get('newPassword2');
  }

  public resetPassword(){
    this.authService.resetPassword({newPassword1:this.newPassword1?.value||'',newPassword2:this.newPassword2?.value||'',token:this.token})
    .subscribe(data =>{
      if (data?.statusCode===200){
        this.router.navigate(['/'])
      }else{
        alert("bir hata oluştu");
      }
    })
  }

}
