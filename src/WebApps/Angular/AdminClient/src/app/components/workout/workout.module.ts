import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExerciseComponent } from './exercise/exercise.component';
import { WorkoutComponent } from './workout/workout.component';
import { RouterModule } from '@angular/router';
import { WorkoutHomeComponent } from './workout.home/workout.home.component';
import { AuthGuard } from 'src/app/guards/auth.guard';



@NgModule({
  declarations: [
    ExerciseComponent,
    WorkoutComponent,
    WorkoutHomeComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: "", component: WorkoutHomeComponent, canActivate: [AuthGuard] },
      { path: "workouts", component: WorkoutComponent, canActivate: [AuthGuard] },
      { path: "exercises", component: ExerciseComponent, canActivate: [AuthGuard] }
    ]),
  ]
})
export class WorkoutModule { }
