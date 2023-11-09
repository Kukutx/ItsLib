import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TokenService } from 'src/app/services/token/token.service';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  hide = true;
  registerForm;
  showError = false;
  errorMessage = "";
  disable = false;

  loginVerify() {
    if (this.tokenService.readToken() !== "") {
      this.router.navigateByUrl("/products");
    }
  }

  constructor(private tokenService: TokenService, private router: Router, private authService: AuthenticationService){
    this.loginVerify();
    this.registerForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      surname: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.pattern('^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*]).{8,20}$')]),
      dateofbirth: new FormControl('', [Validators.required]),
      fiscalcode: new FormControl('', [Validators.required]),
    });
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      this.authService.Register({
        name: this.registerForm.value.name!,
        dateOfBirth: this.registerForm.value.dateofbirth!,
        fiscalCode: this.registerForm.value.fiscalcode!,
        password: this.registerForm.value.password!,
        surname: this.registerForm.value.surname!,
        username: this.registerForm.value.email!
      }).subscribe({
        next: (value) => {
          this.router.navigateByUrl("/login");
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

