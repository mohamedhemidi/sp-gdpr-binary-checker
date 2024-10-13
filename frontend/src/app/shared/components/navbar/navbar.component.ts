import { Component, inject, Input, OnInit } from '@angular/core';
import { AuthService } from '../../../modules/authentication/services/auth.service';
import { Router } from '@angular/router';
import { IUserDetails } from '../../../modules/authentication/types/userDetails';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent {
  authService = inject(AuthService);
  userDetails!: IUserDetails;

  /**
   *
   */
  constructor(private router: Router) {}

  @Input() disableNavbar!: boolean;

  isLoggedIn() {
    return this.authService.isLoggedIn();
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
