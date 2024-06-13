import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Team } from './models/team';
import { Athlete } from '../athletes/models/athlete';

@Injectable({
  providedIn: 'root'
})
export class TeamsService {
  private apiUrl: string = environment.apiUrl + "/teams";
  constructor(private http: HttpClient) { }

  getTeams(): Observable<Team[]>{
    return this.http.get<Team[]>(this.apiUrl);
  }

  getAthletes(id: string): Observable<Athlete[]>{
    return this.http.get<Athlete[]>(`${this.apiUrl}/${id}/athletes`);
  }

  getTeamById(id: string): Observable<Team>{
    return this.http.get<Team>(`${this.apiUrl}/${id}`);
  }

  createTeam(team: Team){
    return this.http.post(`${this.apiUrl}`, team);
  }

  editTeam(id: string, team: Team){
    return this.http.put(`${this.apiUrl}/${id}`, team);
  }

  deleteTeam(id: string){
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
