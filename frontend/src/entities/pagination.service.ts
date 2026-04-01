import { Injectable } from '@angular/core';
import { IRegistration, IAuthorization } from '../interfaces/user.interface';

@Injectable()
export class PaginationService {

  public checkAllString(userData: IRegistration | IAuthorization): { isValid: boolean; errors: string[] } {
    const errors: string[] = [];

    for (const [key, value] of Object.entries(userData)) {
      const fieldError = this.validateField(key, value);
      if (fieldError) {
        errors.push(fieldError);
      }
    }

    return {
      isValid: errors.length === 0,
      errors: errors
    };
  }

  private validateField(fieldName: string, value: any): string | null {
    // Проверка на пустое значение
    if (!value || value.toString().trim() === '') {
      return `${this.getFieldName(fieldName)} не может быть пустым`;
    }

    // Проверка для email (для авторизации и регистрации)
    if (fieldName === 'email') {
      if (!this.isValidEmail(value)) {
        return 'Введите корректный email (пример: user@domain.com)';
      }
    }

    // Проверка для пароля
    if (fieldName === 'password') {
      if (value.length < 6) {
        return 'Пароль должен содержать минимум 6 символов';
      }
    }

    // Проверка для логина (только для регистрации)
    if (fieldName === 'login') {
      if (value.length < 3) {
        return 'Логин должен содержать минимум 3 символа';
      }
      if (!/^[a-zA-Z0-9_]+$/.test(value)) {
        return 'Логин может содержать только буквы, цифры и underscore';
      }
    }

    // Проверка для подтверждения пароля
    if (fieldName === 'confirmPassword') {
      if (value.length < 6) {
        return 'Пароль должен содержать минимум 6 символов';
      }
    }

    return null;
  }

  // Человекочитаемое имя поля
  private getFieldName(fieldName: string): string {
    const names: { [key: string]: string } = {
      'email': 'Email',
      'password': 'Пароль',
      'login': 'Логин',
      'confirmPassword': 'Подтверждение пароля'
    };
    return names[fieldName] || fieldName;
  }

  private isValidEmail(email: string): boolean {
    const emailRegex = /^[^\s@]+@([^\s@.,]+\.)+[^\s@.,]{2,}$/;
    return emailRegex.test(email);
  }

  public validateRegistration(registrationData: IRegistration): { isValid: boolean; errors: string[] } {
    const baseValidation = this.checkAllString(registrationData);
    const errors = [...baseValidation.errors];

    if (registrationData.password !== registrationData.confirmPassword) {
      errors.push('Пароли не совпадают');
    }

    return {
      isValid: errors.length === 0,
      errors: errors
    };
  }

  // Проверка для авторизации (email + password)
  public validateAuthorization(authData: IAuthorization): { isValid: boolean; errors: string[] } {
    return this.checkAllString(authData);
  }
}
