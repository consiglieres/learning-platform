import { Injectable } from '@angular/core';
import { ICourse } from '../interfaces/courses.interface';

export interface ICourseFilters {
  search?: string;
  direction?: string;
  difficulty?: string;
  technology?: string;
}

@Injectable({ providedIn: 'root' })
export class FilterService {

  public applyFilters(courses: ICourse[], filters: ICourseFilters): ICourse[] {
    let filtered = [...courses];

    // Поиск по названию и описанию
    if (filters.search?.trim()) {
      const searchLower = filters.search.toLowerCase();
      filtered = filtered.filter(course =>
        course.title.toLowerCase().includes(searchLower) ||
        course.description.toLowerCase().includes(searchLower)
      );
    }

    // Направление (language)
    if (filters.direction) {
      const dirLower = filters.direction.toLowerCase();
      filtered = filtered.filter(course =>
        course.language?.toLowerCase() === dirLower
      );
    }

    // Технология (категория)
    if (filters.technology) {
      const techLower = filters.technology.toLowerCase();
      filtered = filtered.filter(course =>
        course.categories?.some(cat => cat.value.toLowerCase() === techLower)
      );
    }

    // Сложность / сортировка
    if (filters.difficulty) {
      if (filters.difficulty === 'new') {
        // условно считаем, что новые курсы имеют id > 3 (или по дате, если будет поле)
        filtered = filtered.filter(c => Number(c.id) > 3);
      } else if (filters.difficulty === 'popular') {
        // популярные – длительность больше 30 часов
        filtered = filtered.filter(c => (c.duration ?? 0) > 30);
      }
      // 'all' – без фильтрации
    }

    return filtered;
  }
}
