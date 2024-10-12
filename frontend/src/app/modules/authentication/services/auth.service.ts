import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { env } from '../../../environments/environment.dev';
import { ILoginRequest } from '../types/loginRequest';
import { IResponse } from '../../../shared/types/IResponse';
import { ILoginResponse } from '../types/loginResponse';
import { jwtDecode } from 'jwt-decode';
import { IUserDetails } from '../types/userDetails';
import { AUTH_TOKENS_KEY } from '../constants/keys';
import { ISignupRequest } from '../types/signupRequest';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  authTokensKey: string = AUTH_TOKENS_KEY;

  public $refreshTokenReceive = new Subject<boolean>();

  constructor(private http: HttpClient) {}

  login = (data: ILoginRequest): Observable<IResponse<ILoginResponse>> => {
    return this.http.post<IResponse<ILoginResponse>>(env.Login, data);
  };

  signup = (data: ISignupRequest): Observable<IResponse<ILoginResponse>> => {
    return this.http.post<IResponse<ILoginResponse>>(env.Signup, data);
  };

  isLoggedIn = (): boolean => {
    const accessToken = this.getAuthTokens();
    if (!accessToken?.AccessToken.Token) return false;
    return true;
  };

  logout = (): void => {
    localStorage.removeItem(this.authTokensKey);
  };

  getUserDetails = (): IUserDetails | null => {
    const token = this.getAuthTokens();
    if (!token) return null;
    const decodedToken: any = jwtDecode(token.AccessToken.Token);
    const userDetails = {
      id: decodedToken.sub,
      fullname: decodedToken.name,
      email: decodedToken.email,
    };
    return userDetails || null;
  };

  private isTokenExpired = (): boolean => {
    const accessToken = this.getAuthTokens();
    if (!accessToken) return true;
    const decoded = jwtDecode(accessToken.AccessToken.Token);
    const isTokenExpired = Date.now() >= decoded['exp']! * 1000;
    if (isTokenExpired) this.logout();
    return isTokenExpired;
  };

  //
  //
  // Retrieve Tokens :
  //
  //

  getAuthTokens = (): ILoginResponse | null => {
    const tokens = JSON.parse(localStorage.getItem(this.authTokensKey)!);
    return tokens;
  };
}
