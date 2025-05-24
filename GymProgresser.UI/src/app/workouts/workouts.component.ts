import { Component } from '@angular/core';
import { Workout } from './workout.model';
import { WorkoutService } from './workout.service';
import { WorkoutDetails } from './workout-exercises.model';


@Component({
  selector: 'app-workouts',
  templateUrl: './workouts.component.html',
  styleUrl: './workouts.component.css'
})
export class WorkoutsComponent {
  workouts: WorkoutDetails[] = [];
  newWorkout: WorkoutDetails = {
    date: new Date().toISOString(),
    durationMin: 0,
    note: '',
    exercises: []
  };
  error: string | null = null;

  constructor(private workoutService: WorkoutService) { }

  ngOnInit(): void {
    this.loadWorkouts();
  }

  loadWorkouts(): void {
    this.workoutService.getWorkouts().subscribe({
      next: (data) => (console.log(data), this.workouts = data),
      error: () => (this.error = 'Nie udało się pobrać treningów.')
    });
  }

  addWorkout(): void {
  if (!this.newWorkout.note.trim()) return;

  const workoutToAdd: WorkoutDetails = {
    ...this.newWorkout,
    date: new Date().toISOString(),
    exercises: this.newWorkout.exercises || []
  };

  this.workoutService.addWorkout(workoutToAdd).subscribe({
    next: (workout) => {
      this.workouts.push(workout);
      this.newWorkout = {
        date: new Date().toISOString(),
        durationMin: 0,
        note: '',
        exercises: []
      };
    },
    error: () => {
      this.error = 'Nie udało się dodać treningu.';
    }
  });
}

}
