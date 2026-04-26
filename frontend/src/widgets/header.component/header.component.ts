import {Component, inject, OnInit} from '@angular/core';
import {RouterLink} from '@angular/router';
import {ModalService} from '../../entities/modal.service';
import {UserService} from '../../entities/user.service';
import {AsyncPipe} from '@angular/common';
import {IUserData} from '../../interfaces/user.interface';

@Component({
  selector: 'app-header',
  imports: [
    RouterLink,
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit {
  private _userService = inject(UserService);
  private _modalService = inject(ModalService);

  public check: boolean = false
  public userData!: IUserData;

  ngOnInit() {
    this._userService.userData$.subscribe(result => {
      if(result !== null){
        this.check = true
        this.userData = result
      }
    });
  }


  public showModalRegistration(): void {
    this._modalService.openRegistration();
  }

  public showModalAuthorization(): void {
    this._modalService.openAuthorization();
  }
}
