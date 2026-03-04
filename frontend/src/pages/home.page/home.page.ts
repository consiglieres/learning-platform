import { Component } from '@angular/core';
import { HeaderComponent } from '../../widgets/header.component/header.component';
import { UserService } from '../../entities/user.service';
import { CodeEditor } from '../../widgets/code-editor/code-editor';

@Component({
  selector: 'app-home.page',
  imports: [HeaderComponent, CodeEditor],
  templateUrl: './home.page.html',
  styleUrl: './home.page.scss',
  providers: [UserService],
})
export class HomePage {}
