import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AppSidebarComponent } from '../app-sidebar/app-sidebar.component';
import { AppHeaderComponent } from '../app-header/app-header.component';
import { AppFooterComponent } from '../app-footer/app-footer.component';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [
    AppSidebarComponent,
    AppHeaderComponent,
    AppFooterComponent,
    RouterOutlet,
  ],
  templateUrl: './app-layout.component.html',
  styleUrl: './app-layout.component.css',
})
export class AppLayoutComponent {
  title = 'App Layout Component';
}
