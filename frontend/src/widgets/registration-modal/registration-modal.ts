// import { Component, inject, output, signal } from '@angular/core';
// import { ModalService } from '../../entities/modal.service';
// import { UserService } from '../../entities/user.service';
// import { form, FormField } from '@angular/forms/signals';
// import { IRegistration, IRegistrationModel } from '../../interfaces/user.interface';
// import { take } from 'rxjs';
// import { ValidationService } from '../../entities/validation.service';
//
// @Component({
//   selector: 'app-registration-modal',
//   imports: [FormField],
//   templateUrl: './registration-modal.html',
//   styleUrl: './registration-modal.scss',
//   providers: [ValidationService]
// })
// export class RegistrationModal {
//   public notificationUser = signal<string>('');
//   public colorNotificationText = signal<string>('red');
//
//   private readonly _userService = inject(UserService);
//   private readonly _validationService = inject(ValidationService);
//   private readonly _modalService = inject(ModalService);
//
//   private readonly _registrationDataModel = signal<IRegistrationModel>({
//     login: '',
//     email: '',
//     password: '',
//     confirmPassword: '',
//     confirmPolice: false
//   });
//
//   public registrationForm = form<IRegistrationModel>(this._registrationDataModel);
//
//   closeModal = output<void>();
//
//   public createUser(): void {
//     const formValue = this.registrationForm().value();
//
//     // Формируем объект для валидации и отправки (без login и confirmPolice)
//     const registrationData: IRegistration = {
//       email: formValue.email || '',
//       password: formValue.password || '',
//       confirmPassword: formValue.confirmPassword || ''
//     };
//
//     const checkBox = formValue.confirmPolice;
//
//     // Валидация
//     const validationResult = this._validationService.validateRegistration(registrationData);
//     if (!validationResult.isValid) {
//       this.notificationUser.set(validationResult.errors[0]);
//       return;
//     }
//
//     if (!checkBox) {
//       this.notificationUser.set('Необходимо согласиться с политикой обработки персональных данных');
//       return;
//     }
//
//     // Отправка на сервер
//     this._userService.register(registrationData)
//       .pipe(take(1))
//       .subscribe({
//         next: () => {
//           this.colorNotificationText.set('green');
//           this.notificationUser.set('Вы успешно зарегистрировались');
//
//           setTimeout(() => {
//             this._modalService.openAuthorization();
//           }, 1500);
//         },
//         error: (error) => {
//           // Можно вывести более конкретную ошибку, если сервер возвращает понятное сообщение
//           const serverMessage = error?.error?.message || error?.message;
//           if (serverMessage) {
//             this.notificationUser.set(serverMessage);
//           } else {
//             this.notificationUser.set('Ошибка при регистрации. Попробуйте позже.');
//           }
//           console.error('Registration error:', error);
//         }
//       });
//   }
//
//   public close(): void {
//     this.closeModal.emit();
//   }
//
//   public switchToAuthorization(): void {
//     this._modalService.openAuthorization();
//   }
// }

import { Component, inject, output, signal } from '@angular/core';
import { ModalService } from '../../entities/modal.service';
import { UserService } from '../../entities/user.service';
import { form, FormField } from '@angular/forms/signals';
import { IRegistrationModel } from '../../interfaces/user.interface';
import { take } from 'rxjs';
import { ValidationService } from '../../entities/validation.service';

@Component({
  selector: 'app-registration-modal',
  imports: [FormField],
  templateUrl: './registration-modal.html',
  styleUrls: ['./registration-modal.scss'],
  providers: [ValidationService]
})
export class RegistrationModal {
  public notificationUser = signal<string>('');
  public colorNotificationText = signal<string>('red');

  private readonly _userService = inject(UserService);
  private readonly _validationService = inject(ValidationService);
  private readonly _modalService = inject(ModalService);

  private readonly _registrationDataModel = signal<IRegistrationModel>({
    login: '',
    email: '',
    password: '',
    confirmPassword: '',
    confirmPolice: false
  });

  public registrationForm = form<IRegistrationModel>(this._registrationDataModel);
  closeModal = output<void>();

  public createUser(): void {
    const formValue = this.registrationForm().value() as IRegistrationModel;

    // 1. Валидация всех полей
    const validationResult = this._validationService.validateRegistration(formValue);
    if (!validationResult.isValid) {
      this.notificationUser.set(validationResult.errors[0]);
      return;
    }

    // 2. Проверка чекбокса согласия
    if (!formValue.confirmPolice) {
      this.notificationUser.set(
        'Необходимо согласиться с политикой обработки персональных данных'
      );
      return;
    }

    // 3. Отправка на сервер (локальный мок)
    this._userService.register(formValue)
      .pipe(take(1))
      .subscribe({
        next: () => {
          this.colorNotificationText.set('green');
          this.notificationUser.set('Вы успешно зарегистрировались');

          setTimeout(() => {
            this.closeModal.emit();
            this._modalService.open('authorization');
          }, 1500);
        },
        error: (error) => {
          // В моке ошибка — это Error с message, в реальном API может быть другая структура
          const message = error?.message || 'Ошибка при регистрации';
          this.notificationUser.set(message);
          console.error('Registration error:', error);
        }
      });
  }

  public close(): void {
    this.closeModal.emit();
  }

  public switchToAuthorization(): void {
    this._modalService.open('authorization');
  }
}
