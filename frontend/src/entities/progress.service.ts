// progress.service.ts
import { Injectable } from '@angular/core';
import {ITask} from '../interfaces/courses.interface';

export interface TaskProgress {
  [taskKey: string]: boolean;
}

@Injectable({ providedIn: 'root' })
export class ProgressService {
  private storageKey = 'course_progress';

  getLastTopic(courseId: string): string | null {
    const data = this.getData();
    return data[courseId]?.lastTopicId || null;
  }

  setLastTopic(courseId: string, topicId: string): void {
    const data = this.getData();
    if (!data[courseId]) data[courseId] = { lastTopicId: topicId, tasks: {} };
    else data[courseId].lastTopicId = topicId;
    this.saveData(data);
  }

  getTaskStatus(courseId: string, topicId: string, taskId: string): boolean {
    const key = this.makeTaskKey(courseId, topicId, taskId);
    const data = this.getData();
    return data[courseId]?.tasks?.[key] || false;
  }

  updateTaskStatus(courseId: string, topicId: string, taskId: string, completed: boolean): void {
    const data = this.getData();
    if (!data[courseId]) data[courseId] = { lastTopicId: '', tasks: {} };
    const key = this.makeTaskKey(courseId, topicId, taskId);
    if (completed) data[courseId].tasks[key] = true;
    else delete data[courseId].tasks[key];
    this.saveData(data);
  }

  getTasksWithStatus(courseId: string, topicId: string, tasksFromCourse: ITask[]): ITask[] {
    return tasksFromCourse.map(task => ({
      ...task,
      completed: this.getTaskStatus(courseId, topicId, task.id)
    }));
  }

  private makeTaskKey(courseId: string, topicId: string, taskId: string): string {
    return `${courseId}_${topicId}_${taskId}`;
  }

  private getData(): Record<string, { lastTopicId: string; tasks: Record<string, boolean> }> {
    const raw = localStorage.getItem(this.storageKey);
    return raw ? JSON.parse(raw) : {};
  }

  private saveData(data: any): void {
    localStorage.setItem(this.storageKey, JSON.stringify(data));
  }
}
