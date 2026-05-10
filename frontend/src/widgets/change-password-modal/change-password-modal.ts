import { Component, inject, signal } from '@angular/core';
import { form, FormField } from '@angular/forms/signals';
import { UserService } from '../../entities/user.service';
import { ValidationService } from '../../entities/validation.service';
import { ModalService } from '../../entities/modal.service';
import { IChangePassword } from '../../interfaces/user.interface';
import { take } from 'rxjs';

@Component({
  selector: 'app-change-password-modal',
  imports: [FormField],
  templateUrl: './change-password-modal.html',
  styleUrls: ['./change-password-modal.scss'],
  providers: [ValidationService]
})
export class ChangePasswordModal {
  private _userService = inject(UserService);
  private _validationService = inject(ValidationService);
  private _modalService = inject(ModalService);

  public notification = signal('');
  public colorNotification = signal('red');

  private passwordModel = signal<IChangePassword>({
    currentPassword: '',
    newPassword: '',
    confirmNewPassword: ''
  });

  public passwordForm = form<IChangePassword>(this.passwordModel);

  public submit() {
    const data = this.passwordForm().value() as IChangePassword;
    const validation = this._validationService.validateChangePassword(data);
    if (!validation.isValid) {
      this.notification.set(validation.errors[0]);
      return;
    }

    this._userService.changePassword(data)
      .pipe(take(1))
      .subscribe({
        next: () => {
          this.colorNotification.set('green');
          this.notification.set('Пароль успешно изменён');
          setTimeout(() => this._modalService.closeAll(), 1500);
        },
        error: (err) => {
          this.notification.set(err?.message || 'Ошибка при смене пароля');
        }
      });
  }

  public cancel() {
    this._modalService.closeAll();
  }
}
