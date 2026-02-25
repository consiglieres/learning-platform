import { Component } from '@angular/core';
import { HeaderComponent } from '../../features/header.component/header.component';
import { UserService } from '../../entities/user.service';
import { CodeEditor } from '../../features/code-editor/code-editor';

@Component({
  selector: 'app-home.page',
  imports: [HeaderComponent, CodeEditor],
  templateUrl: './home.page.html',
  styleUrl: './home.page.scss',
  providers: [UserService],
})
export class HomePage {}
