import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  errorMessage = '';
  isRegisterMode = false;

  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
  });

  registerForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
    confirmPassword: ['', Validators.required],
  });

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) { }

  get form(): FormGroup {
    return this.isRegisterMode ? this.registerForm : this.loginForm;
  }

  toggleMode() {
    this.isRegisterMode = !this.isRegisterMode;
    this.errorMessage = '';
    this.form.reset();
  }

  onSubmit() {
    if (this.form.invalid) return;

    const { email, password, confirmPassword } = this.form.value;

    if (this.isRegisterMode) {
      if (password !== confirmPassword) {
        this.errorMessage = 'Hasła nie są zgodne.';
        return;
      }

      this.auth.register({ email, password, confirmPassword }).subscribe({
        next: () => {
          this.errorMessage = '';
          alert('Rejestracja zakończona sukcesem. Możesz się teraz zalogować.');
          this.toggleMode(); // przełącz na logowanie
        },
        error: () => {
          this.errorMessage = 'Rejestracja nie powiodła się. Spróbuj ponownie.';
        }
      });
    } else {
      this.auth.login({ email, password }).subscribe({
        next: (token) => {
          console.log('Otrzymany token:', token);
          this.router.navigate(['/']);
        },
        error: () => {
          this.errorMessage = 'Nieprawidłowy email lub hasło.';
        }
      });
    }
  }
}
