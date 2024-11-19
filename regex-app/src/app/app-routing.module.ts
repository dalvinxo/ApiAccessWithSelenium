import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GamesComponent } from './components/games/games.component';
import { PostsComponent } from './components/posts/posts.component';
import { GameFormComponent } from './components/game-form/game-form.component';

const routes: Routes = [
  { path: '', redirectTo: '/games', pathMatch: 'full' },
  { path: 'games', component: GamesComponent },
  { path: 'games/:id', component: GameFormComponent },
  { path: 'posts', component: PostsComponent },
  { path: '**', redirectTo: '/games' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
