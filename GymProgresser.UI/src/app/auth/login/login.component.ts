import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  errorMessage = '';

  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
  });

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) {}

  onSubmit() {
    if (this.loginForm.invalid) return;

    const { email, password } = this.loginForm.value;
    if (email && password) {
      this.auth.login({ email, password }).subscribe({
        next: (token) => {
          console.log('Otrzymany token:', token);
          this.router.navigate(['/']);
        },
        error: (error) => {
          console.error('Error log in:', error);   
          this.errorMessage = 'Nieprawidłowy email lub hasło';
        }
      });
    } 
  }
}
