import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, firstValueFrom, Observable, of, tap, catchError, throwError } from 'rxjs';
import {
  IAuthorization,
  IRegistrationModel,
  IUserData,
  IChangePassword,
  IChangeEmail
} from '../interfaces/user.interface';

const BASE_URL = 'http://localhost:5172/api/v2/accounts';

@Injectable()   // ← без providedIn: 'root'
export class UserService {
  private readonly _http = inject(HttpClient);

  private readonly _userDataSubject = new BehaviorSubject<IUserData | null>(null);
  public readonly userData$ = this._userDataSubject.asObservable();

  public get currentUser(): IUserData | null {
    return this._userDataSubject.value;
  }

  public register(data: IRegistrationModel): Observable<void> {
    const body = {
      login: data.login,
      email: data.email,
      password: data.password,
      confirmPassword: data.confirmPassword
    };
    return this._http.post<void>(`${BASE_URL}/register`, body);
  }

  public login(data: IAuthorization): Observable<void> {
    const body = {
      email: data.email,
      password: data.password,
      rememberMe: data.rememberMe ?? false
    };
    return this._http.post<void>(`${BASE_URL}/login`, body).pipe(
      tap(() => this._userDataSubject.next(null))
    );
  }

  public logout(): Observable<void> {
    return this._http.post<void>(`${BASE_URL}/logout`, {}).pipe(
      tap(() => this._userDataSubject.next(null))
    );
  }

  public logoutAll(): Observable<void> {
    return this._http.post<void>(`${BASE_URL}/logout-all`, {}).pipe(
      tap(() => this._userDataSubject.next(null))
    );
  }

  public getUserData(): Observable<IUserData> {
    const cached = this._userDataSubject.value;
    if (cached) {
      return of(cached);
    }
    return this._http.get<IUserData>(`${BASE_URL}/me`).pipe(
      tap(user => this._userDataSubject.next(user)),
      catchError(error => throwError(() => error))
    );
  }

  public getUserDataPromise(): Promise<IUserData> {
    return firstValueFrom(this.getUserData());
  }

  public refreshUserData(): Observable<IUserData> {
    this._userDataSubject.next(null);
    return this.getUserData();
  }

  public changePassword(data: IChangePassword): Observable<void> {
    return this._http.post<void>(`${BASE_URL}/change-password`, data);
  }

  public forgotPassword(email: string): Observable<void> {
    return this._http.post<void>(`${BASE_URL}/forgot-password`, { email });
  }

  public confirmEmail(token: string): Observable<void> {
    const params = new HttpParams().set('token', token);
    return this._http.get<void>(`${BASE_URL}/confirm-email`, { params });
  }

  public resendConfirmation(email: string): Observable<void> {
    return this._http.post<void>(`${BASE_URL}/resend-confirmation`, { email });
  }

  public changeEmail(data: IChangeEmail): Observable<void> {
    return this._http.post<void>(`${BASE_URL}/change-email`, data);
  }
}
