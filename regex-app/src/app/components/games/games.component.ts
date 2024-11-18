import { Component } from '@angular/core';
import { GamesService } from 'src/app/services/games.service';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css'],
})
export class GamesComponent {
  games: any[] = [];

  constructor(private gamesService: GamesService) {}

  ngOnInit(): void {
    this.loadGames();
  }

  loadGames(): void {
    this.gamesService.getGames().subscribe(
      (data) => {
        this.games = data;
      },
      (error) => {
        console.error('Error al cargar los juegos:', error);
      }
    );
  }
}
