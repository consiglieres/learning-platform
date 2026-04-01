import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class CookieService {
  public setCookies(token: string): void{
    const date: Date = new Date();

    date.setTime(date.getTime() + (7 * 24 * 60 * 60 * 1000));

    const cookieData: string = `access_token=${token};expires=${date.toUTCString()};path=/;SameSite=Strict`;

    document.cookie = cookieData;
  }

  public getCookies(): any {
    const cookies = document.cookie.split('; ');

    for (let cookie of cookies) {
      const [name, value] = cookie.split('=');
      if (name === 'access_token') {
        return value;
      }
    }

    return null;
  }

  public deleteCookies(): void {
      document.cookie = 'auth_token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
  }
}
