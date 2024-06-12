import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExerciseComponent } from './exercise/exercise.component';
import { WorkoutComponent } from './workout/workout.component';
import { RouterModule } from '@angular/router';
import { WorkoutHomeComponent } from './workout.home/workout.home.component';



@NgModule({
  declarations: [
    ExerciseComponent,
    WorkoutComponent,
    WorkoutHomeComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: "", component: WorkoutHomeComponent },
      { path: "workouts", component: WorkoutComponent },
      { path: "exercises", component: ExerciseComponent }
    ]),
  ]
})
export class WorkoutModule { }
