import { Component } from '@angular/core';

@Component({
  selector: 'app-code-error',
  imports: [],
  templateUrl: './code-error.html',
  styleUrl: './code-error.scss',
})
export class CodeError {
  public error: string = '';

  public setError(): void {}
}
