import { Component, inject } from '@angular/core';
import { LOCAL_STORAGE_KEYS } from '../../constants/local-storage-keys';
import { Router, RouterModule } from '@angular/router';
import { SessionService } from '../../services/session.service';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './app-sidebar.component.html',
  styleUrl: './app-sidebar.component.css',
})
export class AppSidebarComponent {
  private sessionService = inject(SessionService);
  private router = inject(Router);

  logout(): void {
    this.sessionService.logout();
    this.router.navigate(['/login']);
  }
}
