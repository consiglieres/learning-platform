import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {IAuthorization, IRegistration, IUserData} from '../interfaces/user.interface';
import {BehaviorSubject, Observable, tap} from 'rxjs';
import { CookieService } from './cookie.service';

@Injectable()
export class UserService {
  private readonly _urlApi: string = "http://localhost:5172/api/v1/V1Account/"

  private userDataSubject = BehaviorSubject<IUserData>

  private readonly _httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private _http: HttpClient, private _cookieService: CookieService) { }

  public registration (registrationData: IRegistration) : Observable<{ status: string }> {
    return this._http.post<{ status: string }>(this._urlApi + "register", registrationData, this._httpOptions)
  }

  public authorization (authorizationData: IAuthorization): Observable<{ token: string }> {
    return this._http.post<{  error: string, token: string }>( this._urlApi + 'login', authorizationData, this._httpOptions).pipe(tap(
      result => {
      this._cookieService.setCookies(result.token);
    }))
  }

  public getUserData(){
    const token: string = this._cookieService.getCookies()

    this._http.get(this._urlApi + "users/").subscribe()
  }

}
