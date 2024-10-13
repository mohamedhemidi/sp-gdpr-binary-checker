import { Component, inject, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router, withViewTransitions } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
})
export class ProfileComponent implements OnInit {
  profileInfo = {
    email: '',
    name: '',
    id: '',
  };
  /**
   *
   */
  constructor(private router: Router) {}

  authService = inject(AuthService);

  ngOnInit(): void {
    this.profileInfo.email = this.authService.getUserDetails()?.email as string;
    this.profileInfo.name = this.authService.getUserDetails()
      ?.fullname as string;
    this.profileInfo.id = this.authService.getUserDetails()?.id as string;

    if (!this.authService.isLoggedIn()) {
      this.router.navigateByUrl('/auth/login');
    }
  }

  onDeleteAccount() {
    this.authService.deleteAccount().subscribe({
      next: (response) => {
        console.log(response);
        this.authService.logout();
        this.router.navigateByUrl('/');
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
