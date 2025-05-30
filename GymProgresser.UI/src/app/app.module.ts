import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from './auth/login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { ExercisesComponent } from './exercises/exercises.component';
import { ExerciseDetailsComponent } from './exercises/exercise-details/exercise-details.component';
import { WorkoutsComponent } from './workouts/workouts.component';
import { FormsModule } from '@angular/forms';
import { authInterceptor } from './auth/auth.interceptor';
import { AddWorkoutComponent } from './workouts/add-workout/add-workout/add-workout.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MaterialModule } from './shared/material.module';
import { registerLocaleData } from '@angular/common';
import localePl from '@angular/common/locales/pl';
import { WorkoutsHistoryComponent } from './workouts/workouts-history/workouts-history.component';

registerLocaleData(localePl);

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    ExercisesComponent,
    ExerciseDetailsComponent,
    WorkoutsComponent,
    AddWorkoutComponent,
    WorkoutsHistoryComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    MaterialModule,
    
  ],
  providers: [
    provideHttpClient(withInterceptors([authInterceptor])), 
    provideAnimationsAsync(),
    {provide: LOCALE_ID, useValue: 'pl'}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
