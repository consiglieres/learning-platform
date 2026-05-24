import { AfterViewInit, Component, ElementRef, Input, Output, EventEmitter, ViewChild, signal } from '@angular/core';
import { EditorView, keymap, highlightActiveLine, lineNumbers } from '@codemirror/view';
import { EditorState } from '@codemirror/state';
import { defaultHighlightStyle, syntaxHighlighting } from '@codemirror/language';
import { javascript } from '@codemirror/lang-javascript';
import { history, historyKeymap } from '@codemirror/commands';
import { TaskService } from '../../entities/task.service';
import {TaskNotification} from '../../features/task-notification/task-notification';

@Component({
  selector: 'app-code-editor',
  standalone: true,
  imports: [TaskNotification],
  templateUrl: './code-editor.html',
  styleUrls: ['./code-editor.scss'],
  providers: [TaskService]
})
export class CodeEditor implements AfterViewInit {
  @ViewChild('editor', { static: true }) editor!: ElementRef;
  @Input() starterCode = '';
  @Input() expectedSolution = '';
  @Input() taskDescription = '';
  @Input() language = 'javascript';
  @Output() codeCompleted = new EventEmitter<boolean>();

  private editorView!: EditorView;
  resultMessage = signal('');
  resultType = signal<'success' | 'error' | ''>('');
  isChecking = signal(false);

  constructor(private taskService: TaskService) {}

  ngAfterViewInit() {
    const updateListener = EditorView.updateListener.of(() => {});
    this.editorView = new EditorView({
      parent: this.editor.nativeElement,
      state: EditorState.create({
        doc: this.starterCode,
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

  runCode(): void {
    if (!this.editorView) return;
    const userCode = this.editorView.state.doc.toString();
    this.isChecking.set(true);
    this.taskService.checkCode(userCode, this.expectedSolution).subscribe({
      next: (result) => {
        this.resultMessage.set(result.message);
        this.resultType.set(result.success ? 'success' : 'error');
        this.isChecking.set(false);
        if (result.success) {
          this.codeCompleted.emit(true);
        }
      },
      error: () => {
        this.resultMessage.set('Ошибка при проверке кода');
        this.resultType.set('error');
        this.isChecking.set(false);
      }
    });
  }
}
