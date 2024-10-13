import { Component, Input, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-body',
  templateUrl: './body.component.html',
  styleUrl: './body.component.scss',
})
export class BodyComponent implements OnInit {
  @Input() collapsed = false;
  @Input() screenWidth = 0;
  @Input() disableSidebar!: boolean;

  disableNavbar!: boolean;

  /**
   *
   */
  constructor(private router: Router) {}

  getBodyClass(): string {
    let styleClass = '';
    if (this.disableSidebar == true) {
      return 'disableSidebar';
    }
    if (this.collapsed && this.screenWidth > 768) {
      return 'body-trimmed';
    } else if (
      this.collapsed &&
      this.screenWidth <= 768 &&
      this.screenWidth > 0
    ) {
      return 'body-md-screen';
    }
    return styleClass;
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
          this.disableNavbar = true;
        } else {
          this.disableNavbar = false;
        }
      }
    });
  }
}
