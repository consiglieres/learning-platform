import {Component, effect, inject, signal} from '@angular/core';
import { FooterComponent } from '../../../widgets/footer.component/footer.component';
import { HeaderComponent } from '../../../widgets/header.component/header.component';
import {NavigationEnd, Router, RouterOutlet} from '@angular/router';
import {RegistrationModal} from '../../../widgets/registration-modal/registration-modal';
import {AuthorizationModal} from '../../../widgets/authorization-modal/authorization-modal';
import {ModalService} from '../../../entities/modal.service';
import {ChangePasswordModal} from '../../../widgets/change-password-modal/change-password-modal';
import {ChangeEmailModal} from '../../../widgets/change-email-modal/change-email-modal';
import {filter, Subscription} from 'rxjs';
import {NgClass} from '@angular/common';

@Component({
  selector: 'app-main-layout',
  imports: [HeaderComponent, FooterComponent, RouterOutlet, RegistrationModal, AuthorizationModal, ChangePasswordModal, ChangeEmailModal],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.scss',
})
export class MainLayout {
  private modalService = inject(ModalService);
  private _router = inject(Router);

  // Получаем сигнал из сервиса
  public activeModal = this.modalService.activeModal;

  public headerMode = signal<'absolute' | 'relative'>('absolute');
  private routerSub?: Subscription;


  ngOnInit(): void {
    // Определяем начальный режим
    this.setHeaderMode(this._router.url);
    // Отслеживаем смену маршрута
    this.routerSub = this._router.events
      .pipe(filter(e => e instanceof NavigationEnd))
      .subscribe((e: NavigationEnd) => {
        this.setHeaderMode(e.urlAfterRedirects || e.url);
      });
  }

  private setHeaderMode(url: string): void {
    // Если текущий путь содержит '/profile' (настройте под свою структуру)
    this.headerMode.set(url.includes('/profile') ? 'relative' : 'absolute');
  }

  public closeModals(): void {
    this.modalService.closeAll();
  }

  ngOnDestroy(): void {
    this.routerSub?.unsubscribe();
  }
}
