import {Component, inject, OnInit} from '@angular/core';
import {RouterLink} from '@angular/router';
import {ModalService} from '../../entities/modal.service';
import {UserService} from '../../entities/user.service';
import {AsyncPipe} from '@angular/common';

@Component({
  selector: 'app-header',
  imports: [
    RouterLink,
    AsyncPipe,
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  private _userService = inject(UserService);
  private _modalService = inject(ModalService);

  public userData$ = this._userService.userData$

  public showModalRegistration(): void {
    this._modalService.openRegistration();
  }

  public showModalAuthorization(): void {
    this._modalService.openAuthorization();
  }
}
