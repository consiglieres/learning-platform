import {Injectable} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';

export interface Breadcrumb {
  label: string;
  url: string;
}

@Injectable({providedIn: 'root'})

export class BreadcrumbsService {
  constructor(private router: Router, private route: ActivatedRoute) {}

  public buildBreadcrumbs() {
    let breadcrumbs: Breadcrumb[] = [];
    let currentRoute = this.route.root;
    let url = '';

    while (currentRoute.firstChild) {
      currentRoute = currentRoute.firstChild;

      const routeSnapshot = currentRoute.snapshot;

      if (routeSnapshot.data['breadcrumb']) {
        url += '/' + routeSnapshot.url.map(seg => seg.path).join('/');
        breadcrumbs.push({
          label: routeSnapshot.data['breadcrumb'],
          url
        });
      }
    }

    return breadcrumbs;
  }

}
