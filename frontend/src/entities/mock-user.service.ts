import { Injectable } from '@angular/core';
import { BehaviorSubject, firstValueFrom, Observable, of, throwError } from 'rxjs';
import { delay } from 'rxjs/operators';
import {
  IAuthorization,
  IRegistrationModel,
  IUserData,
  IChangePassword,
  IChangeEmail
} from '../interfaces/user.interface';
import {ITask} from '../interfaces/courses.interface';

const STORAGE_KEY = 'mock_users';
const SESSION_KEY = 'mock_session';

interface StoredUser extends IUserData {
  login: string;
  password: string;
}

@Injectable()
export class MockUserService {
  private readonly _userDataSubject = new BehaviorSubject<IUserData | null>(null);
  public readonly userData$ = this._userDataSubject.asObservable();

  public get currentUser(): IUserData | null {
    return this._userDataSubject.value;
  }

  constructor() {
    this.restoreSession();
  }

  public register(data: IRegistrationModel): Observable<void> {
    if (data.password !== data.confirmPassword) {
      return throwError(() => new Error('Пароли не совпадают')).pipe(delay(300));
    }
    const users = this.getStoredUsers();
    if (users.some(u => u.email === data.email)) {
      return throwError(() => new Error('Пользователь с таким email уже существует')).pipe(delay(300));
    }
    const newUser: StoredUser = {
      id: Date.now(),
      login: data.login,
      email: data.email,
      password: data.password,
      name: '',
      phone: '',
      img: '',
      dateConnection: new Date().toISOString(),
      courseComplete: ''
    };
    users.push(newUser);
    this.saveUsers(users);
    return of(undefined).pipe(delay(300));
  }

  public login(data: IAuthorization): Observable<void> {
    const users = this.getStoredUsers();
    const found = users.find(u => u.email === data.email && u.password === data.password);
    if (!found) {
      return throwError(() => new Error('Неверный email или пароль')).pipe(delay(300));
    }
    localStorage.setItem(SESSION_KEY, found.email);
    this._userDataSubject.next(this.toPublicUser(found));
    return of(undefined).pipe(delay(300));
  }

  public logout(): Observable<void> {
    localStorage.removeItem(SESSION_KEY);
    this._userDataSubject.next(null);
    return of(undefined).pipe(delay(100));
  }

  public logoutAll(): Observable<void> {
    return this.logout();
  }

  public getUserData(): Observable<IUserData> {
    const cached = this._userDataSubject.value;
    if (cached) return of(cached).pipe(delay(100));
    const email = localStorage.getItem(SESSION_KEY);
    if (email) {
      const users = this.getStoredUsers();
      const found = users.find(u => u.email === email);
      if (found) {
        const publicUser = this.toPublicUser(found);
        this._userDataSubject.next(publicUser);
        return of(publicUser).pipe(delay(100));
      }
    }
    return throwError(() => new Error('Пользователь не авторизован')).pipe(delay(100));
  }

  public getUserDataPromise(): Promise<IUserData> {
    return firstValueFrom(this.getUserData());
  }

  public refreshUserData(): Observable<IUserData> {
    this._userDataSubject.next(null);
    return this.getUserData();
  }

  public changePassword(data: IChangePassword): Observable<void> {
    const publicUser = this._userDataSubject.value;
    if (!publicUser) return throwError(() => new Error('Не авторизован')).pipe(delay(100));
    if (data.newPassword !== data.confirmNewPassword) {
      return throwError(() => new Error('Пароли не совпадают')).pipe(delay(300));
    }
    const users = this.getStoredUsers();
    const stored = users.find(u => u.email === publicUser.email);
    if (!stored || stored.password !== data.currentPassword) {
      return throwError(() => new Error('Текущий пароль неверен')).pipe(delay(300));
    }
    stored.password = data.newPassword;
    this.saveUsers(users);
    return of(undefined).pipe(delay(300));
  }

  public changeEmail(data: IChangeEmail): Observable<void> {
    const publicUser = this._userDataSubject.value;
    if (!publicUser) return throwError(() => new Error('Не авторизован')).pipe(delay(100));
    const users = this.getStoredUsers();
    if (users.some(u => u.email === data.newEmail)) {
      return throwError(() => new Error('Пользователь с таким email уже существует')).pipe(delay(300));
    }
    const stored = users.find(u => u.email === publicUser.email)!;
    stored.email = data.newEmail;
    this.saveUsers(users);
    localStorage.setItem(SESSION_KEY, data.newEmail);
    this._userDataSubject.next(this.toPublicUser(stored));
    return of(undefined).pipe(delay(300));
  }

  public forgotPassword(email: string): Observable<void> {
    const users = this.getStoredUsers();
    if (!users.some(u => u.email === email)) {
      return throwError(() => new Error('Пользователь с таким email не найден')).pipe(delay(300));
    }
    return of(undefined).pipe(delay(300));
  }

  public confirmEmail(token: string): Observable<void> {
    return of(undefined).pipe(delay(100));
  }

  public resendConfirmation(email: string): Observable<void> {
    return of(undefined).pipe(delay(100));
  }

  private getStoredUsers(): StoredUser[] {
    const raw = localStorage.getItem(STORAGE_KEY);
    return raw ? JSON.parse(raw) : [];
  }

  private saveUsers(users: StoredUser[]): void {
    localStorage.setItem(STORAGE_KEY, JSON.stringify(users));
  }

  private toPublicUser(stored: StoredUser): IUserData {
    const { password, ...publicUser } = stored;
    return publicUser as IUserData;
  }

  private restoreSession(): void {
    const email = localStorage.getItem(SESSION_KEY);
    if (email) {
      const users = this.getStoredUsers();
      const stored = users.find(u => u.email === email);
      if (stored) {
        this._userDataSubject.next(this.toPublicUser(stored));
      } else {
        localStorage.removeItem(SESSION_KEY);
      }
    }
  }

  private generateTasksForTopic(topicId: string, isFirstTopic: boolean = false): ITask[] {
    if (isFirstTopic) {
      return [
        { id: `${topicId}_task1`, title: 'Задание 1', points: 10, type: 'code', completed: false},
      ];
    }

    return [
      { id: `${topicId}_task1`, title: 'Задание 1', points: 10, type: 'theory', completed: false },
      { id: `${topicId}_task2`, title: 'Задание 2', points: 25, type: 'theory', completed: false },
      { id: `${topicId}_task3`, title: 'Задание 3', points: 50, type: 'theory', completed: false },
    ];
  }
}
