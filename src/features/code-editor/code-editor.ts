import { Component, inject, signal } from '@angular/core';
import { ICodeEditor } from '../../interfaces/code-editor.interface';
import { form, FormField } from '@angular/forms/signals';
import { TaskService } from '../../entities/task.service';

@Component({
  selector: 'app-code-editor',
  imports: [FormField],
  templateUrl: './code-editor.html',
  providers: [TaskService],
  styleUrl: './code-editor.scss',
})
export class CodeEditor {
  private _taskService = inject(TaskService);
  private codeModel = signal<ICodeEditor>({
    code: '',
  });

  public codeEditor = form(this.codeModel);

  public runCode(): void {
    console.log(this.codeEditor.code().value());
    this._taskService.checkCode(this.codeEditor.code().value()).subscribe((response) => {
      console.log(response);
    });
  }
}
