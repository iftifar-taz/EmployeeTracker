import { Routes } from '@angular/router';
import { AppLayoutComponent } from './components/app-layout/app-layout.component';
import { AuthLayoutComponent } from './components/auth-layout/auth-layout.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ForgotPasswordComponent } from './pages/forgot-password/forgot-password.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { ResetPasswordComponent } from './pages/reset-password/reset-password.component';
import { PublicUrlGuard } from './guards/public-url.guard';
import { ProtectedUrlGuard } from './guards/protected-url.guard';
import { DepartmentsComponent } from './pages/departments/departments.component';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'login',
  },
  {
    path: '',
    component: AuthLayoutComponent,
    canActivate: [PublicUrlGuard],
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'forgot-password', component: ForgotPasswordComponent },
      { path: 'reset-password', component: ResetPasswordComponent },
    ],
  },
  {
    path: '',
    component: AppLayoutComponent,
    canActivate: [ProtectedUrlGuard],
    children: [
      { path: 'dashboard', component: DashboardComponent },
      { path: 'departments', component: DepartmentsComponent },
    ],
  },
  {
    path: '**',
    redirectTo: 'login',
  },
];
