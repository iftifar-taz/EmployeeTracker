import { inject } from '@angular/core';
import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SessionService } from '../services/session.service';
import { LoginResponse } from '../interfaces/login-response';
import { LOCAL_STORAGE_KEYS } from '../constants/local-storage-keys';

export const httpErrorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const snackBar = inject(MatSnackBar);
  const sessionService = inject(SessionService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      let errorMessage = 'An unknown error occurred!';

      if (error.error instanceof ErrorEvent) {
        // Client-side error
        errorMessage = `Error: ${error.error.message}`;
      } else {
        // Server-side error
        switch (error.status) {
          case 400:
            errorMessage = error.error.message;
            break;
          case 401:
            errorMessage = 'Unauthorized. Please login again.';

            // Try refreshing the token
            return sessionService.refreshToken().pipe(
              switchMap((res: LoginResponse) => {
                localStorage.setItem(LOCAL_STORAGE_KEYS.TOKEN, res.token);
                localStorage.setItem(
                  LOCAL_STORAGE_KEYS.REFRESH_TOKEN,
                  res.refreshToken
                );

                return next(
                  req.clone({
                    setHeaders: {
                      'Content-Type': 'application/json',
                      Authorization: `Bearer ${res.token}`,
                    },
                  })
                );
              }),
              catchError((refreshError) => {
                // Handle error in token refresh (i.e., refresh token expired or invalid)
                snackBar.open('Session expired. Please login again.', 'Close', {
                  duration: 5000,
                });
                sessionService.logout();
                router.navigate(['/login']); // Redirect to login page
                return throwError(
                  () => new Error('Session expired. Please login again.')
                );
              })
            );
          case 403:
            errorMessage = "Forbidden! You don't have permission.";
            break;
          case 404:
            errorMessage = 'Resource not found!';
            break;
          case 500:
            errorMessage = 'Internal Server Error. Please try again later.';
            break;
          default:
            errorMessage = `Unexpected Error: ${error.status}`;
        }
      }

      // Optionally display an error message via Snackbar or Toast
      snackBar.open(errorMessage, 'Close', { duration: 5000 });

      return throwError(() => new Error(errorMessage)); // Return the error
    })
  );
};
