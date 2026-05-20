import { AfterViewInit, Component, ElementRef, signal, ViewChild } from '@angular/core';
import { ICodeEditor } from '../../interfaces/code-editor.interface';
import { form, FormField } from '@angular/forms/signals';
import { TaskService } from '../../entities/task.service';
import { EditorView, keymap, highlightActiveLine, lineNumbers } from '@codemirror/view';
import { EditorState } from '@codemirror/state';
import { defaultHighlightStyle, syntaxHighlighting } from '@codemirror/language';
import { javascript } from '@codemirror/lang-javascript';
import { history, historyKeymap } from '@codemirror/commands';

@Component({
  selector: 'app-code-editor',
  imports: [],
  templateUrl: './code-editor.html',
  providers: [TaskService],
  styleUrl: './code-editor.scss',
})
export class CodeEditor implements AfterViewInit {
  @ViewChild('editor', { static: true }) editor!: ElementRef;
  constructor(private _taskService: TaskService) {}
  private codeModel = signal<ICodeEditor>({
    code: '',
  });

  public codeEditor = form(this.codeModel);

  ngAfterViewInit() {
    const updateListener = EditorView.updateListener.of((update) => {
      if (update.docChanged) {
        const value = update.state.doc.toString();
        this.codeModel.update(() => ({ ...this.codeModel(), code: value }));
      }
    });

    new EditorView({
      parent: this.editor.nativeElement,
      state: EditorState.create({
        doc: this.codeEditor.code().value(),
        extensions: [
          lineNumbers(),
          updateListener,
          history(),
          keymap.of(historyKeymap),
          javascript(),
          syntaxHighlighting(defaultHighlightStyle),
          highlightActiveLine(),
        ],
      }),
    });
  }

  public runCode(): void {
    console.log(this.codeEditor.code().value());
    const code = this.codeEditor.code().value();
    this._taskService.checkCode(code).subscribe((response) => {
      console.log(response);
    });
  }
}
