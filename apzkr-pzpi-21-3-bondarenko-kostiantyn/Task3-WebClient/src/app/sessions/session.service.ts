import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Session } from './models/session';
import { PerformanceMetric } from './models/performancemetric';
import { HealthMetric } from './models/healthmetric';

@Injectable({
  providedIn: 'root'
})
export class SessionService {
  private apiUrl: string = environment.apiUrl + "/sessions";

  constructor(private http: HttpClient) { }

  getSessions(teamId: string): Observable<Session[]>{
    return this.http.get<Session[]>(`${this.apiUrl}?teamId=${teamId}`);
  }

  getSessionById(id: string): Observable<Session>{
    return this.http.get<Session>(`${this.apiUrl}/${id}`);
  }

  getPerformanceMetrics(sessionId: string, athleteId: string, metricType: string): Observable<PerformanceMetric[]>{
    return this.http.get<PerformanceMetric[]>(`${this.apiUrl}/${sessionId}/performanceMetrics?athleteId=${athleteId}&metricType=${metricType}`);
  }

  getHealthMetrics(sessionId: string, athleteId: string, metricType: string): Observable<HealthMetric[]>{
    return this.http.get<HealthMetric[]>(`${this.apiUrl}/${sessionId}/healthmetrics?athleteId=${athleteId}&metricType=${metricType}`);
  }

  createSession(session: Session): Observable<Session>{
    return this.http.post<Session>(`${this.apiUrl}`, session);
  }

  deleteSession(id: string){
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
