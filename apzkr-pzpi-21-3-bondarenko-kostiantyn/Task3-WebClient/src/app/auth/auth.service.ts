import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LoginModel } from './models/login.model';
import { SignupModel } from './models/signup.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl: string = environment.apiUrl + "/auth";
  private tokenKey = 'auth_token';
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient) { }

  login(loginModel: LoginModel): Observable<any>{
    return this.http.post<any>(`${this.apiUrl}/login`, loginModel)
      .pipe(
        tap(response => {
          if (response && response.accessToken) {
            this.setToken(response.accessToken);
            this.isAuthenticatedSubject.next(true);
          }
        })
      );;
  }

  signup(signupModel: SignupModel): Observable<any>{
    return this.http.post(`${this.apiUrl}/signup`, signupModel);
  }

  private setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }
}
