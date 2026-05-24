import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { MockCourseService } from '../../entities/mock-course.service';
import { ProgressService } from '../../entities/progress.service';
import { ITask } from '../../interfaces/courses.interface';
import {CodeEditor} from '../../widgets/code-editor/code-editor';

@Component({
  selector: 'app-topic',
  standalone: true,
  imports: [CommonModule, CodeEditor],
  templateUrl: './topic.html',
  styleUrls: ['./topic.scss'],
  providers: [MockCourseService]
})
export class Topic implements OnInit {
  private route = inject(ActivatedRoute);
  private courseService = inject(MockCourseService);
  private progressService = inject(ProgressService);

  protected _pageState: 'theme' | 'task' = 'theme';

  topicTitle = signal('');
  topicDescription = signal('');
  tasks = signal<ITask[]>([]);
  loading = signal(true);
  selectedTask = signal<ITask | null>(null);

  get completedCount(): number {
    return this.tasks().filter(t => t.completed).length;
  }

  ngOnInit(): void {
    const courseId = this.route.parent?.snapshot.paramMap.get('courseId')!;
    if (!courseId) {
      console.error('courseId not found');
      return;
    }
    const topicId = this.route.snapshot.paramMap.get('themeId')!;
    console.log('🔍 courseId:', courseId, 'topicId:', topicId);
    this.courseService.getCourseById(courseId).subscribe({
      next: (course) => {
        let foundTopic: any = null;
        console.log('📦 Course loaded:', course);
        for (const module of course.modules || []) {
          const topic = module.topics?.find((t: any) => t.id === topicId);
          if (topic) { foundTopic = topic; break; }
        }
        if (foundTopic) {
          this.topicTitle.set(foundTopic.title);
          this.topicDescription.set(foundTopic.description);
          const tasksFromCourse = foundTopic.tasks || [];
          const tasksWithStatus = this.progressService.getTasksWithStatus(courseId, topicId, tasksFromCourse);
          this.tasks.set(tasksWithStatus);
        }
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  toggleTask(task: ITask): void {
    const newStatus = !task.completed;
    task.completed = newStatus;
    const courseId = this.route.snapshot.paramMap.get('courseId')!;
    const topicId = this.route.snapshot.paramMap.get('themeId')!;
    this.progressService.updateTaskStatus(courseId, topicId, task.id, newStatus);
    this.tasks.set([...this.tasks()]);
  }

  openTask(task: ITask): void {
    this.selectedTask.set(task);
    this.setPageStateTask();
  }

  closeCodeEditor(): void {
    this.selectedTask.set(null);
    this.setPageStateTheme(); // меняем вкладку на Теорию (как на ваших скриншотах)
  }

  onCodeCompleted(success: boolean): void {
    const task = this.selectedTask();
    if (success && task && !task.completed) {
      this.toggleTask(task);
      // Можно оставить редактор открытым или сбросить выбор
      // this.selectedTask.set(null);
      // this.setPageStateTheme();
    }
  }

  setPageStateTheme(): void {
    this._pageState = 'theme';
  }

  setPageStateTask(): void {
    this._pageState = 'task';
  }
}
