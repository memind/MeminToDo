import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArtComponent } from './art/art.component';
import { SongComponent } from './song/song.component';
import { RouterModule } from '@angular/router';
import { SkillHomeComponent } from './skill.home/skill.home.component';



@NgModule({
  declarations: [
    ArtComponent,
    SongComponent,
    SkillHomeComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: "", component: SkillHomeComponent },
      { path: "arts", component: ArtComponent},
      { path: "songs", component: SongComponent}
    ]),
  ]
})
export class SkillModule { }
