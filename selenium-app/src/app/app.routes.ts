import { Routes } from '@angular/router';
import { JuegosComponent } from './juegos/juegos.component';
import { PostComponent } from './post/post.component';

export const routes: Routes = [
  { path: 'juegos', component: JuegosComponent },
  { path: 'post', component: PostComponent },
  { path: '', redirectTo: '/juegos', pathMatch: 'full' },
];
