import { Injectable, signal } from '@angular/core';
import { TModal } from '../interfaces/modal.interface';

@Injectable({ providedIn: 'root' })
export class ModalService {
  private activeModalSignal = signal<TModal>(null);
  public activeModal = this.activeModalSignal.asReadonly();

  /** Единый метод открытия любой модалки */
  public open(type: TModal): void {
    this.activeModalSignal.set(type);
  }

  /** Закрыть все */
  public closeAll(): void {
    this.activeModalSignal.set(null);
  }

  /** Проверка, открыта ли конкретная модалка */
  public isModalOpen(type: TModal): boolean {
    return this.activeModalSignal() === type;
  }
}
