import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: "", loadChildren: () => import("./components/dashboard/dashboard.module").then(module => module.DashboardModule), canActivate: [AuthGuard] },
  { path: "my-budget", loadChildren: () => import("./components/budget/budget.module").then(module => module.BudgetModule), canActivate: [AuthGuard] },
  { path: "my-entertainment", loadChildren: () => import("./components/entertainment/entertainment.module").then(module => module.EntertainmentModule), canActivate: [AuthGuard] },
  { path: "my-meal", loadChildren: () => import("./components/meal/meal.module").then(module => module.MealModule), canActivate: [AuthGuard] },
  { path: "my-skill", loadChildren: () => import("./components/skill/skill.module").then(module => module.SkillModule), canActivate: [AuthGuard] },
  { path: "my-workout", loadChildren: () => import("./components/workout/workout.module").then(module => module.WorkoutModule), canActivate: [AuthGuard] } 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }