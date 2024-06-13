import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SessionItemComponent } from './session-item/session-item.component';

const routes: Routes = [
  {path: 'teams/:teamId/sessions/:id', component: SessionItemComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SessionRoutingModule { }
