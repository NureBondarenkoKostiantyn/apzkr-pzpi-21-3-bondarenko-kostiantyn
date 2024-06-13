import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AthleteRoutingModule } from './athlete.routing.module';
import { AthleteListComponent } from './athlete-list/athlete-list.component';
import { AthleteItemComponent } from './athlete-item/athlete-item.component';

@NgModule({
  declarations: [
    AthleteListComponent,
    AthleteItemComponent
  ],
  imports: [
    CommonModule,
    AthleteRoutingModule
  ]
})
export class AthleteModule { }
