import {Component, inject} from '@angular/core';
import { FooterComponent } from '../../../widgets/footer.component/footer.component';
import { HeaderComponent } from '../../../widgets/header.component/header.component';
import { RouterOutlet } from '@angular/router';
import {RegistrationModal} from '../../../widgets/registration-modal/registration-modal';
import {AuthorizationModal} from '../../../widgets/authorization-modal/authorization-modal';
import {ModalService} from '../../../entities/modal.service';
import {ChangePasswordModal} from '../../../widgets/change-password-modal/change-password-modal';
import {ChangeEmailModal} from '../../../widgets/change-email-modal/change-email-modal';

@Component({
  selector: 'app-main-layout',
  imports: [HeaderComponent, FooterComponent, RouterOutlet, RegistrationModal, AuthorizationModal, ChangePasswordModal, ChangeEmailModal],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.scss',
})
export class MainLayout {
  private modalService = inject(ModalService);

  // Получаем сигнал из сервиса
  public activeModal = this.modalService.activeModal;

  // Метод для закрытия модальных окон
  public closeModals(): void {
    this.modalService.closeAll();
  }

}
