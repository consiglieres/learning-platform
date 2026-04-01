import {Component, inject, output, signal} from '@angular/core';
import {ModalService} from '../../entities/modal.service';
import {UserService} from '../../entities/user.service';
import {form, FormField} from '@angular/forms/signals';
import {IRegistration, IRegistrationModel} from '../../interfaces/user.interface';
import {take} from 'rxjs';
import {PaginationService} from '../../entities/pagination.service';

@Component({
  selector: 'app-registration-modal',
  imports: [
    FormField
  ],
  templateUrl: './registration-modal.html',
  styleUrl: './registration-modal.scss',
  providers: [UserService, PaginationService]
})
export class RegistrationModal {
  public notificationUser = signal<string>('')
  public colorNotificationText = signal<string>('red')

  private _registrationDataModel = signal<IRegistrationModel>({
    login: '',
    email: '',
    password: '',
    confirmPassword: '',
    confirmPolice: false
  })

  public registrationForm = form<IRegistrationModel>(this._registrationDataModel)


  private _modalService = inject(ModalService);
  private _userService = inject(UserService);
  private _paginationService = inject(PaginationService);

  closeModal = output<void>();

  public createUser(){
    let formData: IRegistration = {
      login: this.registrationForm().value().login,
      email: this.registrationForm().value().email,
      password: this.registrationForm().value().password,
      confirmPassword: this.registrationForm().value().confirmPassword,
    }

    let checkBox: boolean = this.registrationForm().value().confirmPolice

    const validationResult = this._paginationService.validateRegistration(formData);

    if (!validationResult.isValid) {
      this.notificationUser.set(validationResult.errors[0]);
      return;
    }

    if(!checkBox){
      this.notificationUser.set('Необходимо согласиться с политикой обработки персональных данных');
      return;
    }

    this._userService.registration(formData).pipe(take(1)).subscribe({
      next: (result) => {
        this.colorNotificationText.set('green');
        this.notificationUser.set('Вы успешно зарегистрировались');

        setTimeout(() => {
          this._modalService.openAuthorization();
        }, 1500);
      },
      error: (error) => {
        this.notificationUser.set('Ошибка при регистрации. Попробуйте позже.');
        console.error('Registration error:', error);
      }
    });
  }


  public close(): void {
      this.closeModal.emit()
  }

  public switchToAuthorization(): void {
    this._modalService.openAuthorization();
  }

}
