import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class FilterService {
  public filters(filtersData: Object) {
    console.log(filtersData);
  }
}
