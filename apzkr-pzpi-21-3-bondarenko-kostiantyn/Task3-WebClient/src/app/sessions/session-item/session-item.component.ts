import { Component, OnInit } from '@angular/core';
import { Session } from '../models/session';
import { SessionService } from '../session.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Color, ScaleType } from '@swimlane/ngx-charts';
import { Athlete } from 'src/app/athletes/models/athlete';
import { TeamsService } from 'src/app/teams/team.service';

@Component({
  selector: 'app-session-item',
  templateUrl: './session-item.component.html',
  styleUrls: ['./session-item.component.scss']
})
export class SessionItemComponent implements OnInit {
  session!: Session;
  athletes: Athlete[] = [];
  displayedColumns: string[] = ["email", "firstName", "lastName"];
  userSelected: boolean = false;
  heartMetrics: Array<any> = [];
  speedMetrics: Array<any> = [];
  distanceMetrics: Array<any> = [];
  caloricBurnMetrics: Array<any> = [];
  exerciseDurationMetrics: Array<any> = [];

  constructor(
    private sessionService: SessionService,
    private teamService: TeamsService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(x => {
      let id = x['id']
      this.getSessionById(id);
    });
  }

  getSessionById(id: string){
    this.sessionService.getSessionById(id).subscribe(x => {
      this.session = x;
      this.getTeamAthletes(this.session.teamId);
    })
  }

  deleteSession(){
    this.sessionService.deleteSession(this.session.id).subscribe(x => {
      this.router.navigateByUrl(`/teams/${this.session.teamId}`)
    })
  }

  getTeamAthletes(teamId: string){
    this.teamService.getAthletes(teamId).subscribe(x => {
      this.athletes = x;
    })
  }

  onSelectUser(athleteId: string){
    this.userSelected = true;

    this.sessionService.getHealthMetrics(this.session.id, athleteId, 'HeartRate').subscribe(x => {
      this.heartMetrics = x;
      var data = this.heartMetrics.map((val, index) => ({ "name": (index + 1).toString(), "value": val.metricValue }));
      this.heartRateData = [
        {
          "name": "Speed",
          "series": data
        }
      ]
    })

    this.sessionService.getPerformanceMetrics(this.session.id, athleteId, 'Speed').subscribe(x => {
      this.speedMetrics = x;
      var data = this.speedMetrics.map((val, index) => ({ "name": (index + 1).toString(), "value": val.metricValue }));
      this.speedData = [
        {
          "name": "Speed",
          "series": data
        }
      ]
    })

    // this.sessionService.getPerformanceMetrics(this.session.id, athleteId, 'Distance').subscribe(x => {
    //   this.speedMetrics = x;
    //   var data = this.speedMetrics.map((val, index) => ({ "name": (index + 1).toString(), "value": val.metricValue }));
    //   this.speedData = data;
    // })
  }

  view: [number, number] = [700, 400];
  centeredView: [number, number] = [700, 400];

  gradient = false;
  showLegend = true;
  showLabels = true;
  explodeSlices = false;
  doughnut = false;
  roundEdges = true;

  colorScheme: Color = {
    name: 'light',
    selectable: true,
    group: ScaleType.Ordinal,
    domain: ['#80CBC4', '#C5E1A5', '#FFF59D', '#FFCC80', '#FFAB91', '#BCAAA4']
  };

  heartRateData: any = [];
  distanceData: any = [];
  speedData: any = [];
  caloricBurnData = [];
  exerciseDurationData = [];
}
