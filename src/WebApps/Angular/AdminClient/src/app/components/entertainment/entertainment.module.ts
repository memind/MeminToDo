import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameComponent } from './game/game.component';
import { BookComponent } from './book/book.component';
import { ShowComponent } from './show/show.component';
import { BookNoteComponent } from './book.note/book.note.component';
import { RouterModule } from '@angular/router';
import { EntertainmentHomeComponent } from './entertainment.home/entertainment.home.component';
import { AuthGuard } from 'src/app/guards/auth.guard';



@NgModule({
  declarations: [
    GameComponent,
    BookComponent,
    ShowComponent,
    BookNoteComponent,
    EntertainmentHomeComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: "", component: EntertainmentHomeComponent, canActivate: [AuthGuard] },
      { path: "books", component: BookComponent, canActivate: [AuthGuard] },
      { path: "book-notes", component: BookNoteComponent, canActivate: [AuthGuard] },
      { path: "games", component: GameComponent, canActivate: [AuthGuard] },
      { path: "shows", component: ShowComponent, canActivate: [AuthGuard] },
    ]),
  ]
})
export class EntertainmentModule { }
