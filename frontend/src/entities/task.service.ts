import { Injectable } from '@angular/core';
import {Observable, of} from 'rxjs';
import {delay} from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class TaskService {
  checkCode(userCode: string, expectedSolution: string): Observable<{ success: boolean; message: string }> {
    const normalize = (code: string) => code.replace(/\s+/g, ' ').trim();
    const isEqual = normalize(userCode) === normalize(expectedSolution);
    const result = isEqual
      ? { success: true, message: 'Решение верное!' }
      : { success: false, message: 'Решение неверное. Попробуйте ещё раз.' };
    return of(result).pipe(delay(500));
  }

}
