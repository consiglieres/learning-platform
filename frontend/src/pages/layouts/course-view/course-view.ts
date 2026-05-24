import {Component, inject, OnInit, signal} from '@angular/core';
import {NavigationEnd, Router, RouterOutlet} from '@angular/router';
import {HeaderComponent} from '../../../widgets/header.component/header.component';
import {FooterComponent} from '../../../widgets/footer.component/footer.component';
import {filter, Subscription} from 'rxjs';

@Component({
  selector: 'app-course-view',
  imports: [
    RouterOutlet,
    HeaderComponent,
    FooterComponent
  ],
  templateUrl: './course-view.html',
  styleUrl: './course-view.scss',
})
export class CourseView implements OnInit{
  private _router = inject(Router)

  public headerMode = signal<'absolute' | 'relative'>('absolute');
  private routerSub?: Subscription;

  ngOnInit() {
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
    this.headerMode.set(url.includes('/topic') ? 'relative' : 'absolute');
  }

}
