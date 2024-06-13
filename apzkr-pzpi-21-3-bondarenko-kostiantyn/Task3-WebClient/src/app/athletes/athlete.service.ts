import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Athlete } from './models/athlete';

@Injectable({
  providedIn: 'root'
})
export class AthleteService {
  private apiUrl: string = environment.apiUrl + "/athletes";

  constructor(private http: HttpClient) { }

  getAthletes(): Observable<Athlete[]>{
    return this.http.get<Athlete[]>(`${this.apiUrl}/athletes`);
  }

  getAthleteById(id: string): Observable<Athlete>{
    return this.http.get<Athlete>(`${this.apiUrl}/athletes/${id}`);
  }
}
