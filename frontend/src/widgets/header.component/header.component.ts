// import {Component, inject, OnInit} from '@angular/core';
// import {RouterLink} from '@angular/router';
// import {ModalService} from '../../entities/modal.service';
// import {UserService} from '../../entities/user.service';
// import {AsyncPipe} from '@angular/common';
// import {IUserData} from '../../interfaces/user.interface';
//
// @Component({
//   selector: 'app-header',
//   imports: [
//     RouterLink,
//   ],
//   templateUrl: './header.component.html',
//   styleUrl: './header.component.scss',
// })
// export class HeaderComponent implements OnInit {
//   private _userService = inject(UserService);
//   private _modalService = inject(ModalService);
//
//   public check: boolean = false
//   public userData!: IUserData;
//
//   ngOnInit() {
//     this._userService.userData$.subscribe(result => {
//       if(result !== null){
//         this.check = true
//         this.userData = result
//       }
//     });
//   }
//
//
//   public showModalRegistration(): void {
//     this._modalService.openRegistration();
//   }
//
//   public showModalAuthorization(): void {
//     this._modalService.openAuthorization();
//   }
// }
import { Component, inject, signal, DestroyRef, OnInit } from '@angular/core';
import { RouterLink, Router, NavigationEnd } from '@angular/router';
import { AsyncPipe } from '@angular/common';
import { filter } from 'rxjs/operators';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ModalService } from '../../entities/modal.service';
import { UserService } from '../../entities/user.service';

@Component({
  selector: 'app-header',
  imports: [RouterLink, AsyncPipe],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  private readonly _modalService = inject(ModalService);
  private readonly _userService = inject(UserService);
  private readonly _router = inject(Router);
  private readonly _destroyRef = inject(DestroyRef);

  public readonly userData$ = this._userService.userData$;

  public isProfilePage = signal(false);

  ngOnInit() {
    this._router.events
      .pipe(
        filter((e): e is NavigationEnd => e instanceof NavigationEnd),
        takeUntilDestroyed(this._destroyRef)
      )
      .subscribe((event: NavigationEnd) => {
        this.isProfilePage.set(event.urlAfterRedirects.includes('/codevia/profile'));
      });
  }

  public showModalRegistration(): void {
    this._modalService.open('registration');
  }

  public showModalAuthorization(): void {
    this._modalService.open('authorization');
  }
}
