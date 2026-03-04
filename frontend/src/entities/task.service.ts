import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class TaskService {
  private readonly url: string = '';

  private _http = inject(HttpClient);

  public checkCode(code: string): Observable<any> {
    return this._http.post(this.url, code);
  }
}
