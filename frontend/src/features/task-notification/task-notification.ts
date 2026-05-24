import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-task-notification',
  standalone: true,
  templateUrl: './task-notification.html',
  styleUrls: ['./task-notification.scss']
})
export class TaskNotification {
  @Input() message = '';
  @Input() type: 'success' | 'error' | '' = '';
}
