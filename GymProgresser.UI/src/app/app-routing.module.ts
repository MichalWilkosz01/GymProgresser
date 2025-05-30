import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { HomeComponent } from './home/home.component';
import { ExercisesComponent } from './exercises/exercises.component';
import { ExerciseDetailsComponent } from './exercises/exercise-details/exercise-details.component';
import { WorkoutsComponent } from './workouts/workouts.component';
import { AddWorkoutComponent } from './workouts/add-workout/add-workout/add-workout.component';
import { WorkoutsHistoryComponent } from './workouts/workouts-history/workouts-history.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', component: HomeComponent },
  { path: 'exercises', component: ExercisesComponent },
  { path: 'exercises/:id', component: ExerciseDetailsComponent },
  { path: 'workouts', component: WorkoutsComponent },
  { path: 'workouts/add', component: AddWorkoutComponent },
  { path: 'workouts/history', component: WorkoutsHistoryComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
