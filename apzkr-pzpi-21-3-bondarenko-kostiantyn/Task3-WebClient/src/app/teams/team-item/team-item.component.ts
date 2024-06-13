import { Component, OnInit } from '@angular/core';
import { TeamsService } from '../team.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Team } from '../models/team';
import { MatDialog } from '@angular/material/dialog';
import { EditTeamDialogComponent } from '../dialogs/edit-team-dialog/edit-team-dialog.component';
import { Session } from 'src/app/sessions/models/session';
import { SessionService } from 'src/app/sessions/session.service';
import { CreateSessionDialogComponent } from 'src/app/sessions/dialogs/create-session-dialog/create-session-dialog.component';

@Component({
  selector: 'app-team-item',
  templateUrl: './team-item.component.html',
  styleUrls: ['./team-item.component.scss']
})
export class TeamItemComponent implements OnInit {
  team!: Team;
  sessions: Session[] = [];
  displayedSessionColumns : string[] = ['duration', 'date', 'endDate'];

  constructor(
    private teamService: TeamsService,
    private sessionService: SessionService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(x => {
      let id = x['id']
      this.getTeamById(id);
    });
  }

  getTeamById(id: any){
    this.teamService.getTeamById(id).subscribe(x => {
      this.team = x;
      this.getSessions();
    });
  }

  onNavigateToEditDialog(){
    let dialogRef = this.dialog.open(EditTeamDialogComponent, {
      width: '40rem',
      data: {
        team: this.team
      }
    })
    dialogRef.afterClosed().subscribe(x => {
      this.getTeamById(this.team.id);
    })
  }

  deleteTeam(){
    this.teamService.deleteTeam(this.team.id).subscribe(x => {
      this.router.navigateByUrl('/teams');
    });
  }

  getSessions(){
    this.sessionService.getSessions(this.team.id).subscribe(x => {
      this.sessions = x;
    })
  }

  onNavigateToCreateSessionDialog(){
    let dialogRef = this.dialog.open(CreateSessionDialogComponent, {
      width: '40rem',
      data: {
        teamId: this.team.id
      }
    })
    dialogRef.afterClosed().subscribe(x => {
      this.getTeamById(this.team.id);
    })
  }

  onNavigateToSessionPage(sessionId: string){
    this.router.navigateByUrl(`/teams/${this.team.id}/sessions/${sessionId}`);
  }
}
