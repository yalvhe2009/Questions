import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/question',
        name: '题库',
        iconClass: 'fa fa-book',
        order: 2,
        layout: eLayoutType.application
      },
      {
        path: '/subjects',
        name: '科目',
        parentName: '题库',
        layout: eLayoutType.application
      },
      {
        path: '/questions',
        name: '题目',
        parentName: '题库',
        layout: eLayoutType.application
      }
    ]);
  };
}
