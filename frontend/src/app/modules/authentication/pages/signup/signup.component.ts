import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.scss',
})
export class SignupComponent {
  form: FormGroup;
  usersService = inject(AuthService);
  loginFormSubscription!: Subscription;
  error: string = '';
  signupSuccess: boolean = false;
  validationErrors: { PropertyName: string; ErrorMessage: string }[] = [];

  /**
   *
   */
  constructor(
    private fb: FormBuilder,
    private router: Router,
  ) {
    this.form = this.fb.group({
      Email: [null, [Validators.required, Validators.email]],
      FullName: [null, [Validators.required]],
      Password: [null, [Validators.required]],
      ConfirmPassword: [null, Validators.required],
    });
  }

  ngOnDestroy(): void {
    if (this.loginFormSubscription) {
      this.loginFormSubscription.unsubscribe();
    }
  }

  onSubmit() {
    if (
      !this.form.value.Email ||
      !this.form.value.FullName ||
      !this.form.value.Password ||
      !this.form.value.ConfirmPassword
    ) {
      this.error = 'All fields are required';
      return;
    }
    if (this.form.value.Password !== this.form.value.ConfirmPassword) {
      this.error = 'Password do not match';
      return;
    }
    this.loginFormSubscription = this.usersService
      .signup({
        Email: this.form.value.Email,
        FullName: this.form.value.FullName,
        Password: this.form.value.Password,
        ConfirmPassword: this.form.value.ConfirmPassword,
      })
      .subscribe({
        next: (response) => {
          //this.router.navigateByUrl('/login');
          this.signupSuccess = true;
        },
        error: (err) => {
          console.log('===== SIGNUP ERROR ======', err);
          this.error = err.error.Message;
          if (err.error.StatusCode === 422) {
            this.validationErrors = err.error.Response;
          }
        },
      });
  }
}
