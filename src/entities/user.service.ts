import { Injectable } from '@angular/core';
import { of, filter, map, tap } from 'rxjs';

export interface Movie {
  title: string;
  year: number;
  genre: string[];
  rating: number;
}

@Injectable()
export class UserService {}
