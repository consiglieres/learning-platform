import {Component, inject} from '@angular/core';
import {RouterLink} from '@angular/router';
import {CookieService} from '../../entities/cookie.service';
import {ModalService} from '../../entities/modal.service';

@Component({
  selector: 'app-header',
  imports: [
    RouterLink,
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
  providers: [CookieService]
})
export class HeaderComponent {
  public tokenStatus: boolean = false;

  private _modalService = inject(ModalService);
  private _cookieService = inject(CookieService);

  constructor (){
    this.checkToken()
  }

  private checkToken() {
    const token = this._cookieService.getCookies()
    token !== "" && " " && null ? this.tokenStatus = true : false
  }

  public showModalRegistration(): void {
    this._modalService.openRegistration();
  }

  public showModalAuthorization(): void {
    this._modalService.openAuthorization();
  }
}
