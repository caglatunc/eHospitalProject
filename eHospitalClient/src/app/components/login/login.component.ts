import { Component, ElementRef, ViewChild } from '@angular/core';
import { LoginModel } from '../../models/login.model';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormValidateDirective } from 'form-validate-angular';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, FormValidateDirective],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  @ViewChild("password") password: ElementRef<HTMLInputElement> | undefined;

  isShowPassword: boolean = false;
  loginModel: LoginModel = new LoginModel();

  constructor(
    private http: HttpClient,
    private router: Router) { }

  showOrHidePassword() {
    this.isShowPassword = !this.isShowPassword;

    if (this.isShowPassword) {
      this.password!.nativeElement.type = "text";
    } else {
      this.password!.nativeElement.type = "password";
    }
  }

  login(form: NgForm) {
    if (form.valid) {
      this.http.post("https://localhost:7204/api/Auth/Login", this.loginModel).subscribe({
        next: (res: any) => {
          localStorage.setItem("token", res.data.token);
          this.router.navigateByUrl("/");
        },
        error: (err: HttpErrorResponse) => {
          console.log(err);
        }
      })
    }
  }
}
