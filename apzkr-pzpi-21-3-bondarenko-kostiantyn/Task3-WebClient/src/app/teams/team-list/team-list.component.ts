import { Component, OnInit } from '@angular/core';
import { TeamsService } from '../team.service';
import { Team } from '../models/team';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { CreateTeamDialogComponent } from '../dialogs/create-team-dialog/create-team-dialog.component';

@Component({
  selector: 'app-team-list',
  templateUrl: './team-list.component.html',
  styleUrls: ['./team-list.component.scss']
})
export class TeamListComponent implements OnInit {
  teams: Team[] = [];
  displayedColumns: string[] = ['name', 'country', 'sport'];

  constructor(
    private teamService: TeamsService,
    private router: Router,
    private dialog: MatDialog) { }

  ngOnInit(): void {
    this.getTeams();
  }

  getTeams(){
    this.teamService.getTeams().subscribe(x => {
      console.log(x);
      this.teams = x;
    })
  }

  navigateToTeam(id: string){
    this.router.navigateByUrl(`/teams/${id}`);
  }

  navigateToCreateTeamDialog(){
    var dialogRef = this.dialog.open(CreateTeamDialogComponent, {
      width: '40rem'
    });
    dialogRef.afterClosed().subscribe(x => {
      this.getTeams();
    })
  }
}
