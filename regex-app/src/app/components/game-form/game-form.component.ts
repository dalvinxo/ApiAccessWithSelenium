import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Country } from 'src/app/models/country.model';
import { Game } from 'src/app/models/game.model';
import { GameForm } from 'src/app/models/gameForm.model';
import { CountriesService } from 'src/app/services/countries.service';
import { GamesService } from 'src/app/services/games.service';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-game-form',
  templateUrl: './game-form.component.html',
  styleUrls: ['./game-form.component.css'],
})
export class GameFormComponent {
  countries: Country[] = [];

  gameForm!: FormGroup;

  game!: Game;
  gameId!: number;
  gameName: string = '';

  constructor(
    private fb: FormBuilder,
    private postService: PostService,
    private countryService: CountriesService,
    private gamesService: GamesService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.gameId = +this.route.snapshot.paramMap.get('id')!;

    this.loadCountries();
    this.loadGames(this.gameId);
    // this.gameName = `Juego ${this.gameId}`;

    this.gameForm = this.fb.group({
      name: ['', Validators.required],
      genre: ['', Validators.required],
      age: [null, [Validators.required, Validators.min(0)]],
      paisId: ['', Validators.required],
      title: ['', Validators.required],
      description: ['', Validators.required],
      rate: [
        null,
        [Validators.required, Validators.min(1), Validators.max(10)],
      ],
      cedula: [
        '',
        [
          Validators.required,
          Validators.pattern(/^\d{3}-\d{7}-\d{1}$/),
        ],
      ],
      telefono: [
        '',
        [
          Validators.required,
          Validators.pattern(
            /^\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$/
          ),
        ],
      ],
    });
  }

  loadGames(id:number): void {
    this.gamesService.getGamesById(id).subscribe((data) => {
      this.game = data; 
      this.gameName = data.title;
    });
  }

  loadCountries(): void {
    this.countryService.getCountries().subscribe((data) => {
      this.countries = data;     
    });
  }

  onSubmit(): void {
    if (this.gameForm.valid) {
      const formData: GameForm = {
        ...this.gameForm.value,
        gameId: this.gameId,
      };
      this.postService.createPost(formData).subscribe(
        (response) => {
          console.log('Post creado:', response);
          this.router.navigate(['/games']);
        },
        (error) => console.error('Error al crear el post:', error)
      );
    } else {
      console.error('Formulario inválido');
    }
  }

  goBack(): void {
    this.router.navigate(['/games']);
  }
}
