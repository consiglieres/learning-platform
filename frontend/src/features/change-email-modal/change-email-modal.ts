import { Component, inject, signal } from '@angular/core';
import { form, FormField } from '@angular/forms/signals';
import { UserService } from '../../entities/user.service';
import { ValidationService } from '../../entities/validation.service';
import { ModalService } from '../../entities/modal.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-change-email-modal',
  imports: [FormField],
  templateUrl: './change-email-modal.html',
  styleUrls: ['./change-email-modal.scss'],
  providers: [ValidationService]
})
export class ChangeEmailModal {
  private _userService = inject(UserService);
  private _validationService = inject(ValidationService);
  private _modalService = inject(ModalService);

  public notification = signal('');
  public colorNotification = signal('red');

  private emailModel = signal({
    newEmail: '',
    confirmEmail: ''
  });

  public emailForm = form(this.emailModel);

  public submit() {
    const fv = this.emailForm().value();
    const newEmail = fv.newEmail || '';
    const confirmEmail = fv.confirmEmail || '';

    if (!newEmail || !confirmEmail) {
      this.notification.set('Заполните все поля');
      return;
    }
    if (newEmail !== confirmEmail) {
      this.notification.set('Email адреса не совпадают');
      return;
    }

    const validation = this._validationService.validateChangeEmail({ newEmail });
    if (!validation.isValid) {
      this.notification.set(validation.errors[0]);
      return;
    }

    this._userService.changeEmail({ newEmail })
      .pipe(take(1))
      .subscribe({
        next: () => {
          this.colorNotification.set('green');
          this.notification.set('Почта успешно изменена');
          setTimeout(() => this._modalService.closeAll(), 1500);
        },
        error: (err) => {
          this.notification.set(err?.message || 'Ошибка при смене email');
        }
      });
  }

  public cancel() {
    this._modalService.closeAll();
  }
}
