import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameComponent } from './game/game.component';
import { BookComponent } from './book/book.component';
import { ShowComponent } from './show/show.component';
import { BookNoteComponent } from './book.note/book.note.component';
import { RouterModule } from '@angular/router';
import { EntertainmentHomeComponent } from './entertainment.home/entertainment.home.component';



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
      { path: "", component: EntertainmentHomeComponent },
      { path: "books", component: BookComponent },
      { path: "book-notes", component: BookNoteComponent },
      { path: "games", component: GameComponent },
      { path: "shows", component: ShowComponent },
    ]),
  ]
})
export class EntertainmentModule { }
