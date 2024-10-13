import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { AUTH_TOKENS_KEY } from '../../constants/keys';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  usersService = inject(AuthService);
  loginFormSubscription!: Subscription;
  error: string = '';
  signupRedirectMessage: string = '';

  /**
   *
   */
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private activatedRoute: ActivatedRoute,
  ) {
    this.form = this.fb.group({
      email: [null, [Validators.required, Validators.email]],
      password: [null, Validators.required],
    });
  }
  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe((params) => {
      const signedup = params['signedup'];
      if (signedup == true)
        this.signupRedirectMessage = 'Successfully signed up!';
    });
  }

  ngOnDestroy(): void {
    if (this.loginFormSubscription) {
      this.loginFormSubscription.unsubscribe();
    }
  }

  onSubmit() {
    if (!this.form.value.email || !this.form.value.password) {
      this.error = 'Email and password are required';
      return;
    }
    this.loginFormSubscription = this.usersService
      .login({
        email: this.form.value.email,
        password: this.form.value.password,
      })
      .subscribe({
        next: (response) => {
          localStorage.setItem(
            AUTH_TOKENS_KEY,
            JSON.stringify(response.Response),
          );
          this.router.navigateByUrl('/dashboard');
        },
        error: (err) => {
          this.error = err.error.Message;
        },
      });
  }
}
