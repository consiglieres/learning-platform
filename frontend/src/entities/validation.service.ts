import { Injectable } from '@angular/core';
import { IRegistrationModel, IAuthorization, IChangePassword, IChangeEmail } from '../interfaces/user.interface';

export interface IValidationResult {
  isValid: boolean;
  errors: string[];
}

@Injectable()
export class ValidationService {

  public checkAllString(userData: IAuthorization | IRegistrationModel): IValidationResult {
    const errors: string[] = [];
    for (const [key, value] of Object.entries(userData)) {
      if (key === 'confirmPolice') continue;
      const fieldError = this.validateField(key, value);
      if (fieldError) errors.push(fieldError);
    }
    return { isValid: errors.length === 0, errors };
  }

  private validateField(fieldName: string, value: any): string | null {
    if (value === null || value === undefined || value.toString().trim() === '') {
      return `${this.getFieldName(fieldName)} не может быть пустым`;
    }
    if (fieldName === 'email') {
      if (!this.isValidEmail(value)) {
        return 'Введите корректный email (пример: user@domain.com)';
      }
    }
    if (fieldName === 'password' || fieldName === 'confirmPassword') {
      if (value.length < 6) {
        return 'Пароль должен содержать минимум 6 символов';
      }
    }
    if (fieldName === 'login') {
      if (value.length < 3) {
        return 'Логин должен содержать минимум 3 символа';
      }
      if (!/^[a-zA-Z0-9_]+$/.test(value)) {
        return 'Логин может содержать только буквы, цифры и underscore';
      }
    }
    return null;
  }

  private getFieldName(fieldName: string): string {
    const names: Record<string, string> = {
      email: 'Email',
      password: 'Пароль',
      login: 'Логин',
      confirmPassword: 'Подтверждение пароля'
    };
    return names[fieldName] || fieldName;
  }

  private isValidEmail(email: string): boolean {
    const emailRegex = /^[^\s@]+@([^\s@.,]+\.)+[^\s@.,]{2,}$/;
    return emailRegex.test(email);
  }

  public validateRegistration(data: IRegistrationModel): IValidationResult {
    const baseValidation = this.checkAllString(data);
    const errors = [...baseValidation.errors];
    if (data.password !== data.confirmPassword) {
      errors.push('Пароли не совпадают');
    }
    return { isValid: errors.length === 0, errors };
  }

  public validateAuthorization(authData: IAuthorization): IValidationResult {
    return this.checkAllString(authData);
  }

  public validateChangePassword(data: IChangePassword): IValidationResult {
    const errors: string[] = [];
    if (!data.currentPassword) errors.push('Введите текущий пароль');
    if (!data.newPassword) errors.push('Введите новый пароль');
    else if (data.newPassword.length < 6) errors.push('Новый пароль должен содержать минимум 6 символов');
    if (!data.confirmNewPassword) errors.push('Подтвердите новый пароль');
    else if (data.newPassword !== data.confirmNewPassword) errors.push('Новый пароль и подтверждение не совпадают');
    return { isValid: errors.length === 0, errors };
  }

  public validateChangeEmail(data: IChangeEmail): IValidationResult {
    const errors: string[] = [];
    if (!data.newEmail) {
      errors.push('Введите новый email');
    } else if (!this.isValidEmail(data.newEmail)) {
      errors.push('Некорректный формат email');
    }
    return { isValid: errors.length === 0, errors };
  }

  public validateEmail(email: string): IValidationResult {
    const errors: string[] = [];
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!email) errors.push('Введите email');
    else if (!emailRegex.test(email)) errors.push('Некорректный формат email');
    return { isValid: errors.length === 0, errors };
  }
}
