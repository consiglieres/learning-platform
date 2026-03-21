import {Component, inject, output} from '@angular/core';
import {ModalService} from '../../entities/modal.service';

@Component({
  selector: 'app-authorization-modal',
  imports: [],
  templateUrl: './authorization-modal.html',
  styleUrl: './authorization-modal.scss',
})
export class AuthorizationModal {
  private modalService = inject(ModalService);

  closeModal = output<void>();

  close(): void {
    this.closeModal.emit();
  }

  switchToRegistration(): void {
    this.modalService.openRegistration();
  }

}
