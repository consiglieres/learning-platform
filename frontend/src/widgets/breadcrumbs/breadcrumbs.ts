import { Component } from '@angular/core';
import {NavigationEnd, Router, RouterLink} from '@angular/router';
import {Breadcrumb, BreadcrumbsService} from '../../entities/breadcrumbs.service';
import {filter} from 'rxjs';

@Component({
  selector: 'app-breadcrumbs',
  imports: [
    RouterLink
  ],
  templateUrl: './breadcrumbs.html',
  styleUrl: './breadcrumbs.scss',
})
export class Breadcrumbs {
  public breadcrumbs: Breadcrumb[] = [];

  constructor(
    private router: Router,
    private breadcrumbsService: BreadcrumbsService
  ) {}

  ngOnInit() {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => {
        this.breadcrumbs = this.breadcrumbsService.buildBreadcrumbs();
      });
  }
}
