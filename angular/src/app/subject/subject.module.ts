import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
// import { CommonModule } from '@angular/common';

import { SubjectRoutingModule } from './subject-routing.module';
import { SubjectComponent } from './subject.component';


@NgModule({
  declarations: [
    SubjectComponent
  ],
  imports: [
    // CommonModule,
    SubjectRoutingModule,
    SharedModule
  ]
})
export class SubjectModule { }
