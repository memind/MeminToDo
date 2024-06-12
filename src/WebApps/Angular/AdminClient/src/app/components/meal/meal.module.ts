import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FoodComponent } from './food/food.component';
import { MealComponent } from './meal/meal.component';
import { RouterModule } from '@angular/router';
import { MealHomeComponent } from './meal.home/meal.home.component';
import { AuthGuard } from 'src/app/guards/auth.guard';



@NgModule({
  declarations: [
    FoodComponent,
    MealComponent,
    MealHomeComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: "", component: MealHomeComponent, canActivate: [AuthGuard]},
      { path: "meals", component: MealComponent, canActivate: [AuthGuard]},
      { path: "foods", component: FoodComponent, canActivate: [AuthGuard]}
    ]),
  ]
})
export class MealModule { }
