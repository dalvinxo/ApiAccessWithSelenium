import { Component } from '@angular/core';
import { Game } from 'src/app/models/game.model';
import { GamesService } from 'src/app/services/games.service';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css'],
})
export class GamesComponent {
  games: Game[] = [];

  paginatedGames: any[] = [];
  currentPage: number = 1;
  itemsPerPage: number = 50;
  totalPages: number = 0;

  constructor(private gamesService: GamesService) {}

  ngOnInit(): void {
    this.loadGames();
  }

  loadGames(): void {
    this.gamesService.getGames().subscribe((data) => {
      this.games = data;
      this.totalPages = Math.ceil(this.games.length / this.itemsPerPage); // Calcula el número total de páginas
      this.updatePaginatedGames(); // Actualiza los juegos para la página actual
    });
  }

  updatePaginatedGames(): void {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.paginatedGames = this.games.slice(startIndex, endIndex); // Extrae los juegos para la página actual
  }

  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePaginatedGames();
    }
  }

  navigateToGame(gameUrl: string): void {
    window.open(gameUrl, '_blank');
  }
}
