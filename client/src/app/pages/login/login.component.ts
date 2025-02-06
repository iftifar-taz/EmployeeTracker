import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { SessionService } from '../../services/session.service';
import { LoginRequest } from '../../interfaces/login-request';
import { LoginResponse } from '../../interfaces/login-response';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  private fb = inject(FormBuilder);
  private sessionService = inject(SessionService);
  private router = inject(Router);

  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
  });

  onSubmit() {
    if (this.loginForm.valid) {
      const dto: LoginRequest = {
        email: this.loginForm.value.email!,
        passwordRaw: this.loginForm.value.password!,
      };
      this.sessionService.login(dto).subscribe((res: LoginResponse) => {
        this.router.navigate(['/dashboard']);
      });
    }
  }
}
