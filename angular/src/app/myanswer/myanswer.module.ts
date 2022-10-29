import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MyanswerRoutingModule } from './myanswer-routing.module';
import { MyanswerComponent } from './myanswer.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    MyanswerComponent
  ],
  imports: [
    CommonModule,
    MyanswerRoutingModule,
    SharedModule
  ]
})
export class MyanswerModule { }
