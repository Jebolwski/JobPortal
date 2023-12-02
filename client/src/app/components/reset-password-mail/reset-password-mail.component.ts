import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-reset-password-mail',
  templateUrl: './reset-password-mail.component.html',
  styleUrls: ['./reset-password-mail.component.scss']
})
export class ResetPasswordMailComponent {
  constructor(private router: Router,private authService:AuthenticationService,private route: ActivatedRoute){
  }
  public resetPasswordMailForm: FormGroup = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.email,
      Validators.minLength(6),
      Validators.maxLength(15),
    ]),
  });

  resetPasswordMail(){
    this.authService.resetPasswordMail({email:this.resetPasswordMailForm.get('email')?.value||''})
    .subscribe(data =>{
      if (data?.statusCode===200){
        alert("mail yollandı");
      }else{
        alert("bir hata oluştu");
      }
    })
  }
}
