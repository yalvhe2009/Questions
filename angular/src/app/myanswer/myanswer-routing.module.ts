import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyanswerComponent } from './myanswer.component';

const routes: Routes = [{ path: '', component: MyanswerComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MyanswerRoutingModule { }
