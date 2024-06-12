import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: "", loadChildren: () => import("./components/dashboard/dashboard.module").then(module => module.DashboardModule) },
  { path: "my-budget", loadChildren: () => import("./components/budget/budget.module").then(module => module.BudgetModule) },
  { path: "my-entertainment", loadChildren: () => import("./components/entertainment/entertainment.module").then(module => module.EntertainmentModule) },
  { path: "my-meal", loadChildren: () => import("./components/meal/meal.module").then(module => module.MealModule) },
  { path: "my-skill", loadChildren: () => import("./components/skill/skill.module").then(module => module.SkillModule) },
  { path: "my-workout", loadChildren: () => import("./components/workout/workout.module").then(module => module.WorkoutModule) },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }