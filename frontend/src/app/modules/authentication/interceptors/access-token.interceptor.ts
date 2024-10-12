import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { catchError, switchMap, tap, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { AUTH_TOKENS_KEY } from '../constants/keys';

export const accessTokenInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const authTokensKey = AUTH_TOKENS_KEY;

  const authTokens = authService.getAuthTokens();
  if (!authTokens) return next(req);
  const cloned = req.clone({
    headers: req.headers.set(
      'Authorization',
      'Bearer ' + authTokens.AccessToken.Token,
    ),
  });

  return next(cloned);
};
