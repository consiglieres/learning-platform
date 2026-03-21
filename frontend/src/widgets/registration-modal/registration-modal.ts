import {Component, inject, output} from '@angular/core';
import {ModalService} from '../../entities/modal.service';

@Component({
  selector: 'app-registration-modal',
  imports: [],
  templateUrl: './registration-modal.html',
  styleUrl: './registration-modal.scss',
})
export class RegistrationModal {
  private _modalService = inject(ModalService);

  closeModal = output<void>();

  public close(): void {
    this.closeModal.emit();
  }

  public switchToAuthorization(): void {
    this._modalService.openAuthorization();
  }
}
