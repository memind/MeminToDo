import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BudgetModule } from './components/budget/budget.module';
import { DashboardModule } from './components/dashboard/dashboard.module';
import { EntertainmentModule } from './components/entertainment/entertainment.module';
import { MealModule } from './components/meal/meal.module';
import { SkillModule } from './components/skill/skill.module';
import { WorkoutModule } from './components/workout/workout.module';
import { UiModule } from './ui/ui.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BudgetModule,
    DashboardModule,
    EntertainmentModule,
    MealModule,
    SkillModule,
    WorkoutModule,
    UiModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
