import {Component, inject, output, signal} from '@angular/core';
import {ModalService} from '../../entities/modal.service';
import {IAuthorization} from '../../interfaces/user.interface';
import {form, FormField} from '@angular/forms/signals';
import {UserService} from '../../entities/user.service';
import {take} from 'rxjs';
import {PaginationService} from '../../entities/pagination.service';

@Component({
  selector: 'app-authorization-modal',
  imports: [
    FormField
  ],
  templateUrl: './authorization-modal.html',
  styleUrl: './authorization-modal.scss',
  providers: [UserService, PaginationService],
})
export class AuthorizationModal {
  private _userService = inject(UserService);
  private _paginationService = inject(PaginationService);

  public colorNotificationUser = signal<string>("red")
  public notificationUser = signal<string>('')

  private _userAuthorizationModel = signal<IAuthorization>({
      email: '',
      password: ''
    })

  public userAuthorizationForm  = form<IAuthorization>(this._userAuthorizationModel)

  private modalService = inject(ModalService);

  closeModal = output<void>();

  public authentificationData() {
    let userData: IAuthorization = {
      email: this.userAuthorizationForm().value().email,
      password: this.userAuthorizationForm().value().password
    }

    const validationResult = this._paginationService.validateAuthorization(userData);

    if (!validationResult.isValid) {
      this.notificationUser.set(validationResult.errors[0]);
      this.colorNotificationUser.set('red');
      return;
    }
    else {
      this._userService.authorization(userData).pipe(take(1)).subscribe({
        next: result => {
          this._userService.getUserDataPromise()
          setTimeout(() => {
            this.closeModal.emit()
          }, 1000)
        },
        error: error => {
          this.notificationUser.set("Неверный логин или пароль")
        }
      })
    }

  }

  public close(): void {
      this.closeModal.emit();
  }

  public switchToRegistration(): void {
    this.modalService.openRegistration();
  }

}
