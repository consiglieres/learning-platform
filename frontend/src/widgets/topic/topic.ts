import {Component, input, InputSignal} from '@angular/core';
import {animate, state, style, transition, trigger} from '@angular/animations';
import {ITopic} from '../../interfaces/courses.interface';
import {required} from '@angular/forms/signals';

@Component({
  selector: 'app-topic',
  imports: [],
  templateUrl: './topic.html',
  styleUrl: './topic.scss',
  animations: [
    trigger('expandCollapse', [
      state('collapsed', style({ height: '0', opacity: '0' })),
      state('expanded', style({ height: '*', opacity: '1' })),
      transition('collapsed => expanded', animate('300ms ease')),
      transition('expanded => collapsed', animate('300ms ease'))
    ])
  ]

})
export class Topic {
  //отслеживание состояния
  public expandedTopic: string | null = null;

  public topicInformation = input.required<ITopic>()

  //переключение состояния
  public toggleTopic(title: string): void {
    if (this.expandedTopic === title) {
      this.expandedTopic = null;
    } else {
      this.expandedTopic = title;
    }
  }
}
