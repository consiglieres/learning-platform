// topic.ts
import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { MockCourseService } from '../../entities/mock-course.service';
import { ProgressService } from '../../entities/progress.service';
import { ITask } from '../../interfaces/courses.interface';

@Component({
  selector: 'app-topic',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './topic.html',
  styleUrls: ['./topic.scss'],
  providers: [MockCourseService]
})
export class Topic implements OnInit {
  private route = inject(ActivatedRoute);
  private courseService = inject(MockCourseService);
  private progressService = inject(ProgressService);

  topicTitle = signal('');
  topicDescription = signal('');
  tasks = signal<ITask[]>([]);
  loading = signal(true);

  // Геттер для шаблона
  get completedCount(): number {
    return this.tasks().filter(t => t.completed).length;
  }

  ngOnInit(): void {
    const courseId = this.route.snapshot.paramMap.get('courseId')!;
    const topicId = this.route.snapshot.paramMap.get('themeId')!;
    this.courseService.getCourseById(courseId).subscribe({
      next: (course) => {
        let foundTopic: any = null;
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
}
