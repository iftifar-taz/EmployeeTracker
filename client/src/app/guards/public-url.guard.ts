import { inject, Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { LOCAL_STORAGE_KEYS } from '../constants/local-storage-keys';

@Injectable({
  providedIn: 'root',
})
export class PublicUrlGuard implements CanActivate {
  private router = inject(Router);

  canActivate(): boolean {
    const token = localStorage.getItem(LOCAL_STORAGE_KEYS.TOKEN);
    if (token) {
      this.router.navigate(['/dashboard']);
      return false;
    }

    return true;
  }
}
