import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Sport } from './models/sport';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SportService {
  private apiUrl: string = environment.apiUrl + "/sports";

  constructor(private http: HttpClient) { }

  getSports(): Observable<Sport[]>{
    return this.http.get<Sport[]>(this.apiUrl);
  }
}
