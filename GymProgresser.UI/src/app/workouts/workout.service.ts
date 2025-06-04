import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Workout } from './workout.model';
import { ApiService } from '../core/api.service';
import { WorkoutDetails } from './workout-exercises.model';

@Injectable({ providedIn: 'root' })
export class WorkoutService {
    endpoint = 'Workouts';

    constructor(private api: ApiService) { }

    getWorkouts(): Observable<WorkoutDetails[]> {
        return this.api.get<WorkoutDetails[]>(this.endpoint);
    }

    addWorkout(workout: WorkoutDetails): Observable<WorkoutDetails> {
        return this.api.post<WorkoutDetails>(this.endpoint, workout);
    }

    deleteWorkout(workoutId: number): Observable<void> {
        console.log(`${this.endpoint}/${workoutId}`);
        return this.api.delete<void>(`${this.endpoint}/${workoutId}`);
    }
}
