import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArtComponent } from './art/art.component';
import { SongComponent } from './song/song.component';
import { RouterModule } from '@angular/router';
import { SkillHomeComponent } from './skill.home/skill.home.component';
import { AuthGuard } from 'src/app/guards/auth.guard';



@NgModule({
  declarations: [
    ArtComponent,
    SongComponent,
    SkillHomeComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: "", component: SkillHomeComponent, canActivate: [AuthGuard] },
      { path: "arts", component: ArtComponent, canActivate: [AuthGuard]},
      { path: "songs", component: SongComponent, canActivate: [AuthGuard]}
    ]),
  ]
})
export class SkillModule { }
