import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FoodComponent } from './food/food.component';
import { MealComponent } from './meal/meal.component';
import { RouterModule } from '@angular/router';
import { MealHomeComponent } from './meal.home/meal.home.component';



@NgModule({
  declarations: [
    FoodComponent,
    MealComponent,
    MealHomeComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: "", component: MealHomeComponent},
      { path: "meals", component: MealComponent },
      { path: "foods", component: FoodComponent}
    ]),
  ]
})
export class MealModule { }
