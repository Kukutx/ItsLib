import { Component } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { TokenService } from 'src/app/services/token/token.service';
import { BreakpointObserver } from '@angular/cdk/layout';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm;
  hide = true;
  showError = false;
  errorMessage = "";
  disable = false;

  constructor(private tokenService: TokenService, private router: Router, private fb: FormBuilder, private authService: AuthenticationService) {
    this.loginVerify();
    this.loginForm = fb.group({
      "email": ['',Validators.compose([Validators.required, Validators.email,  Validators.maxLength(60)])],
      "password": ['',Validators.compose([Validators.required, Validators.pattern('^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*]).{8,20}$')])],
    })
  }

  loginVerify() {
    if (this.tokenService.readToken() !== "") {
      this.router.navigate(["/products"]);
    }
  }

  handleLogin() {
    this.showError = false;
    this.disable = true;
    if (this.loginForm.value.email && this.loginForm.value.password) {
      this.authService.Login(this.loginForm.value.email, this.loginForm.value.password).subscribe({
        next: (value) => {
          this.tokenService.setToken(value.token, value.expiration, value.refreshToken);
          this.router.navigate(["/products"]);
        },
        error: (value) => {
        
          if (value.status === 400) {
            this.errorMessage = value.error;
            this.showError = true;
            this.disable = false;
          } else if (value.status === 500) {
            this.errorMessage = "Errore interno del server";
            this.showError = true;
            this.disable = false;
          } else console.log(value);
          
        }
      });
    }
  }
}
