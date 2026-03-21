import {Injectable, signal} from '@angular/core';
import { TModal } from '../interfaces/modal.interface';

@Injectable({providedIn: 'root'})

export class ModalService {
  // Сигнал для отслеживания активного модального окна
  private activeModalSignal = signal<TModal>(null);

  // Публичный readonly сигнал для компонентов
  public activeModal = this.activeModalSignal.asReadonly();

  // Методы для открытия модальных окон
  openRegistration(): void {
    this.activeModalSignal.set('registration');
  }

  openAuthorization(): void {
    this.activeModalSignal.set('authorization');
  }

  // Метод для закрытия всех модальных окон
  closeAll(): void {
    this.activeModalSignal.set(null);
  }

  // Метод для проверки, открыто ли конкретное окно
  isModalOpen(type: TModal): boolean {
    return this.activeModalSignal() === type;
  }
}
