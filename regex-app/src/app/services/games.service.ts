import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Game } from '../models/game.model';

@Injectable({
  providedIn: 'root',
})
export class GamesService {
  private apiUrl: string = environment.apiUrl + '/games';

  constructor(private http: HttpClient) {}

  getGames(): Observable<Game[]> {
    return this.http.get<Game[]>(this.apiUrl);
  }

  getGamesById(id:number): Observable<Game> {
    return this.http.get<Game>(this.apiUrl+`/${id}`);
  }
}
