import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { ISidebarToggle } from './shared/types/SidebarToggle';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'frontend';
  /**
   *
   */
  constructor(private router: Router) {}

  isSidebarCollapsed = false;
  screenWidth = 0;

  disableSidebar = false;

  onToggleSidebar(data: ISidebarToggle): void {
    this.isSidebarCollapsed = data.collapsed;
    this.screenWidth = data.screenWidth;
  }

  ngOnInit(): void {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        if (
          event.url.includes('/login') ||
          event.url.includes('/signup') ||
          this.router.url === '/' ||
          this.router.url === '/terms' ||
          this.router.url === '/privacy'
        ) {
          this.disableSidebar = true;
        } else {
          this.disableSidebar = false;
        }
      }
    });
  }
}
