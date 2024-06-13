import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from './models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl: string = environment.apiUrl + "/users";

  constructor(private http: HttpClient) { }

  getUsers(): Observable<User[]>{
    return this.http.get<User[]>(`${this.apiUrl}`);
  }

  getUserById(id: string): Observable<User>{
    return this.http.get<User>(`${this.apiUrl}/${id}`);
  }
}
