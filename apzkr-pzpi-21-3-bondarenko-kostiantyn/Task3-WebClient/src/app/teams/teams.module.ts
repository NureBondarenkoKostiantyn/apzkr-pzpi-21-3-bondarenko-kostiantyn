import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeamListComponent } from './team-list/team-list.component';
import { TeamItemComponent } from './team-item/team-item.component';
import { BrowserModule } from '@angular/platform-browser';
import { MatTableModule } from '@angular/material/table';
import { TeamRoutingModule } from './team.routing.module';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog'
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input'
import { MatSelectModule } from '@angular/material/select';
import { CreateTeamDialogComponent } from './dialogs/create-team-dialog/create-team-dialog.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EditTeamDialogComponent } from './dialogs/edit-team-dialog/edit-team-dialog.component';

@NgModule({
  declarations: [
    TeamListComponent,
    TeamItemComponent,
    CreateTeamDialogComponent,
    EditTeamDialogComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    TeamRoutingModule,
    MatTableModule,
    MatCardModule,
    MatButtonModule,
    MatDialogModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class TeamsModule { }
