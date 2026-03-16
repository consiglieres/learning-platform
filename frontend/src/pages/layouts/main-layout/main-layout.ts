import { Component } from '@angular/core';
import { FooterComponent } from '../../../widgets/footer.component/footer.component';
import { HeaderComponent } from '../../../widgets/header.component/header.component';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-main-layout',
  imports: [HeaderComponent, FooterComponent, RouterOutlet],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.scss',
})
export class MainLayout {}
