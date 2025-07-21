import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { HomeComponent } from './home/home.component';
import { ExercisesComponent } from './exercises/exercises.component';
import { ExerciseDetailsComponent } from './exercises/exercise-details/exercise-details.component';
import { WorkoutsComponent } from './workouts/workouts.component';
import { WorkoutsHistoryComponent } from './workouts/workouts-history/workouts-history.component';
import { WorkoutFormComponent } from './workouts/workout-form/workout-form/workout-form.component';
import { authGuard } from './core/auth.guard';
import { OneRepMaxComponent } from './one-rep-max/one-rep-max.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', component: HomeComponent },
  { path: 'exercises', component: ExercisesComponent },
  { path: 'exercises/:id', component: ExerciseDetailsComponent },
  { path: 'workouts', component: WorkoutsComponent, canActivate: [authGuard] },
  { path: 'workouts/add', component: WorkoutFormComponent, canActivate: [authGuard] },
  { path: 'workouts/history', component: WorkoutsHistoryComponent, canActivate: [authGuard]},
  { path: 'edit/:id', component: WorkoutFormComponent, canActivate: [authGuard] },
  { path: 'one-rep-max', component: OneRepMaxComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
