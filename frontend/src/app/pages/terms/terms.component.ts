import { Component, inject, OnInit } from '@angular/core';
import { AuthService } from '../../modules/authentication/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-terms',
  templateUrl: './terms.component.html',
  styleUrl: './terms.component.scss',
})
export class TermsComponent implements OnInit {
  authService = inject(AuthService);

  constructor(private router: Router) {}
  ngOnInit(): void {
    if (this.authService.isLoggedIn()) {
      this.router.navigateByUrl('/dashboard');
    }
  }
}
