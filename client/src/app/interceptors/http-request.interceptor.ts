import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { SessionService } from '../services/session.service';
import { LOCAL_STORAGE_KEYS } from '../constants/local-storage-keys';

export const httpRequestInterceptor: HttpInterceptorFn = (req, next) => {
  const token = localStorage.getItem(LOCAL_STORAGE_KEYS.TOKEN);

  if (!token) {
    return next(req);
  }

  return next(
    req.clone({
      setHeaders: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
    })
  );
};
