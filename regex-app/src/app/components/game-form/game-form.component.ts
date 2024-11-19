import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-game-form',
  templateUrl: './game-form.component.html',
  styleUrls: ['./game-form.component.css'],
})
export class GameFormComponent {
  gameForm!: FormGroup;
  gameId!: number;
  gameName: string = '';

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.gameId = +this.route.snapshot.paramMap.get('id')!;
    this.gameName = `Juego ${this.gameId}`;

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
    });
  }

  onSubmit(): void {
    if (this.gameForm.valid) {
      console.log('Formulario enviado:', this.gameForm.value);
      // Lógica para enviar el formulario a la API
    } else {
      console.error('Formulario inválido');
    }
  }

  goBack(): void {
    this.router.navigate(['/games']);
  }
}
