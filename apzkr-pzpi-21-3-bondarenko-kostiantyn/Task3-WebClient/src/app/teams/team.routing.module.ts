import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TeamListComponent } from './team-list/team-list.component';
import { TeamItemComponent } from './team-item/team-item.component';

const routes: Routes = [
  {path: 'teams', component: TeamListComponent, pathMatch: 'full'},
  {path: 'teams/:id', component: TeamItemComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TeamRoutingModule { }
