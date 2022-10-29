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
        path: '/subjects',
        name: '科目',
        layout: eLayoutType.application,
        requiredPolicy: '码小明题库.科目',
        iconClass: 'fa fa-book',
      },
      {
        path: '/questions',
        name: '题目',
        layout: eLayoutType.application,
        requiredPolicy: '码小明题库.题目',
        iconClass: 'fa fa-life-ring',
      },
      {
        path: '/answers',
        name: '答题',
        layout: eLayoutType.application,
        requiredPolicy: '码小明题库.答题',
        iconClass: 'fa fa-pencil'
      },
      {
        path: '/my-answers',
        name: '我的已答',
        layout: eLayoutType.application,
        requiredPolicy: '码小明题库.我的已答',
        iconClass: 'fa fa-user-o'
      }
    ]);
  };
}
