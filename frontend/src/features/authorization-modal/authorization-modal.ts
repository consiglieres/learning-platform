// import { Component, inject, output, signal } from '@angular/core';
// import { ModalService } from '../../entities/modal.service';
// import { IAuthorization } from '../../interfaces/user.interface';
// import { form, FormField } from '@angular/forms/signals';
// import { UserService } from '../../entities/user.service';
// import { take } from 'rxjs';
// import { ValidationService } from '../../entities/validation.service';
//
// @Component({
//   selector: 'app-authorization-modal',
//   imports: [FormField],
//   templateUrl: './authorization-modal.html',
//   styleUrl: './authorization-modal.scss',
//   providers: [ValidationService]
// })
// export class AuthorizationModal {
//   private _userService = inject(UserService);
//   private _validationService = inject(ValidationService);
//   private modalService = inject(ModalService);
//
//   public colorNotificationUser = signal<string>('red');
//   public notificationUser = signal<string>('');
//
//   private _userAuthorizationModel = signal<IAuthorization>({
//     email: '',
//     password: ''
//   });
//
//   public userAuthorizationForm = form<IAuthorization>(this._userAuthorizationModel);
//
//   closeModal = output<void>();
//
//   public authentificationData() {
//     const formValue = this.userAuthorizationForm().value();
//     const userData: IAuthorization = {
//       email: formValue.email || '',
//       password: formValue.password || ''
//     };
//
//     const validationResult = this._validationService.validateAuthorization(userData);
//
//     if (!validationResult.isValid) {
//       this.notificationUser.set(validationResult.errors[0]);
//       this.colorNotificationUser.set('red');
//       return;
//     }
//
//     this._userService.login(userData)
//       .pipe(take(1))
//       .subscribe({
//         next: () => {
//           // После успешного входа загружаем данные пользователя
//           this._userService.getUserDataPromise()
//             .then(() => {
//               this.closeModal.emit();
//             })
//             .catch(() => {
//               // Даже если данные не загрузились, сессия установлена — закрываем
//               this.closeModal.emit();
//             });
//         },
//         error: () => {
//           this.notificationUser.set('Неверный логин или пароль');
//         }
//       });
//   }
//
//   public close(): void {
//     this.closeModal.emit();
//   }
//
//   public switchToRegistration(): void {
//     this.modalService.openRegistration();
//   }
// }


import { Component, inject, output, signal } from '@angular/core';
import { ModalService } from '../../entities/modal.service';
import { IAuthorization } from '../../interfaces/user.interface';
import { form, FormField } from '@angular/forms/signals';
import { UserService } from '../../entities/user.service';
import { take } from 'rxjs';
import { ValidationService } from '../../entities/validation.service';

@Component({
  selector: 'app-authorization-modal',
  imports: [FormField],
  templateUrl: './authorization-modal.html',
  styleUrls: ['./authorization-modal.scss'],
  providers: [ValidationService]
})
export class AuthorizationModal {
  private _userService = inject(UserService);
  private _validationService = inject(ValidationService);
  private _modalService = inject(ModalService);

  public colorNotificationUser = signal<string>('red');
  public notificationUser = signal<string>('');

  private _userAuthorizationModel = signal<IAuthorization>({
    email: '',
    password: ''
  });

  public userAuthorizationForm = form<IAuthorization>(this._userAuthorizationModel);

  closeModal = output<void>();

  public authentificationData() {
    const formValue = this.userAuthorizationForm().value();
    const userData: IAuthorization = formValue as IAuthorization;

    const validationResult = this._validationService.validateAuthorization(userData);

    if (!validationResult.isValid) {
      this.notificationUser.set(validationResult.errors[0]);
      this.colorNotificationUser.set('red');
      return;
    }

    this._userService.login(userData)
      .pipe(take(1))
      .subscribe({
        next: async () => {
          try {
            await this._userService.getUserDataPromise();
          } catch {
          } finally {
            this.closeModal.emit();
          }
        },
        error: () => {
          this.notificationUser.set('Неверный email или пароль');
        }
      });
  }

  public close(): void {
    this.closeModal.emit();
  }

  public switchToRegistration(): void {
    this._modalService.open('registration');
  }
}
