import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AnswerRoutingModule } from './answer-routing.module';
import { AnswerComponent } from './answer.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    AnswerComponent
  ],
  imports: [
    CommonModule,
    AnswerRoutingModule,
    SharedModule
  ]
})
export class AnswerModule { }
