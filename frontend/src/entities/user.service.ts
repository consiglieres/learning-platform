import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IAuthorization, IRegistration } from '../interfaces/user.interface';
import { Observable, tap } from 'rxjs';
import { CookieService } from './cookie.service';

@Injectable()
export class UserService {
  private readonly urlApi: string = ""

  constructor(private _http: HttpClient, private _cookieService: CookieService) { }

  public registration (registrationData: IRegistration) : Observable<{ status: string }> {
    return this._http.post<{ status: string }>('', registrationData)
  }

  public authorization (authorizationData: IAuthorization): Observable<{ token: string }> {
    return this._http.post<{ token: string }>('', authorizationData).pipe(tap(result => {
      this._cookieService.setCookies(result.token);
    }))
  }
}
