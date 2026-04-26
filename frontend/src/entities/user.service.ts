import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {IAuthorization, IRegistration, IUserData} from '../interfaces/user.interface';
import {BehaviorSubject, firstValueFrom, Observable} from 'rxjs';

@Injectable({providedIn: 'root'})
export class UserService {
  private readonly _urlApi: string = "http://localhost:5172/api/v1/V1Account/"

  public _userDataSubject = new BehaviorSubject<IUserData | null>(null);
  public userData$: Observable<IUserData | null> = this._userDataSubject.asObservable()

  private readonly _http = inject(HttpClient)

  public registration (registrationData: IRegistration): Observable<{ status: string }> {
    return this._http.post<{ status: string }>(this._urlApi + "register", registrationData)
  }

  public authorization (authorizationData: IAuthorization): Observable<{ token: string }> {
    return this._http.post<{ error: string, token: string }>( this._urlApi + 'login', authorizationData)
  }

  public getUserDataPromise(): Promise<IUserData> {
    return firstValueFrom(
      this._http.get<IUserData>(this._urlApi + "me")
    ).then(user => {
      this._userDataSubject.next(user);
      return user;
    });
  }
}
