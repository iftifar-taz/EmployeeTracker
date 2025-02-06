import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { LoginRequest } from '../interfaces/login-request';
import { LoginResponse } from '../interfaces/login-response';
import { LOCAL_STORAGE_KEYS } from '../constants/local-storage-keys';
import { UserInformation } from '../interfaces/user-information';

@Injectable({
  providedIn: 'root',
})
export class SessionService {
  private apiUrl = environment.identityServer;
  private http = inject(HttpClient);

  login(dto: LoginRequest): Observable<LoginResponse> {
    return this.http
      .post<LoginResponse>(`${this.apiUrl}/api/v1/sessions`, dto)
      .pipe(
        tap((response: LoginResponse) => {
          localStorage.setItem(LOCAL_STORAGE_KEYS.TOKEN, response.token);
          localStorage.setItem(
            LOCAL_STORAGE_KEYS.REFRESH_TOKEN,
            response.refreshToken
          );
          const claims = JSON.parse(atob(response.token.split('.')[1]));
          const user: UserInformation = {
            userId: claims.nameid,
            email: claims.email,
            role: claims.role,
          };
          localStorage.setItem(LOCAL_STORAGE_KEYS.USER, JSON.stringify(user));
        })
      );
  }

  logout(): void {
    localStorage.removeItem(LOCAL_STORAGE_KEYS.TOKEN);
    localStorage.removeItem(LOCAL_STORAGE_KEYS.REFRESH_TOKEN);
  }

  refreshToken(): Observable<LoginResponse> {
    const dto = {
      email: (
        JSON.parse(
          localStorage.getItem(LOCAL_STORAGE_KEYS.USER)!
        ) as UserInformation
      ).email,
      token: localStorage.getItem(LOCAL_STORAGE_KEYS.TOKEN),
      refreshToken: localStorage.getItem(LOCAL_STORAGE_KEYS.REFRESH_TOKEN),
    };
    return this.http.post<LoginResponse>(
      `${this.apiUrl}/api/v1/sessions/refresh-session`,
      dto
    );
  }
}
