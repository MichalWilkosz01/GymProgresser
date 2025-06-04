import { Component } from '@angular/core';
import { WorkoutService } from './workout.service';
import { WorkoutDetails } from './workout-exercises.model';
import { WorkoutWithState } from './workout-with-state.model';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../shared/confirm-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-workouts',
  templateUrl: './workouts.component.html',
  styleUrl: './workouts.component.css'
})
export class WorkoutsComponent {
  workouts: WorkoutWithState[] = [];
  newWorkout: WorkoutDetails = {
    date: new Date().toISOString(),
    durationMin: 0,
    note: '',
    exercises: []
  };
  error: string | null = null;


  constructor(private workoutService: WorkoutService, private dialog: MatDialog, private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.loadWorkouts();
  }

  loadWorkouts(): void {
    this.workoutService.getWorkouts().subscribe({
      next: (data) => {
        // dodajemy showExercises = false do każdego treningu
        this.workouts = data.map(workout => ({
          ...workout,
          showExercises: false,
          menuOpen: false
        }));
      },
      error: () => (this.error = 'Nie udało się pobrać treningów.')
    });
  }

  toggleExercises(workout: WorkoutDetails & { showExercises?: boolean }): void {
    workout.showExercises = !workout.showExercises;
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


  onSortChange(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    const order = selectElement.value as 'asc' | 'desc';

    this.workouts.sort((a, b) => {
      const dateA = new Date(a.date).getTime();
      const dateB = new Date(b.date).getTime();
      return order === 'asc' ? dateA - dateB : dateB - dateA;
    });
  }

  toggleMenu(workout: WorkoutWithState): void {
    this.workouts.forEach(w => {
      if (w !== workout) w.menuOpen = false;
    });
    workout.menuOpen = !workout.menuOpen;
  }

  editWorkout(workout: WorkoutDetails): void {
    // przykładowa nawigacja (Angular Router)
    // this.router.navigate(['/edit', workout.id]);
    console.log('Edytuj', workout);
  }

  deleteWorkout(workout: WorkoutWithState): void {
    this.dialog.open(ConfirmDialogComponent, {
      width: '320px',
      data: {
        message: `Czy na pewno chcesz usunąć trening z dnia ${new Date(workout.date).toLocaleDateString()}?`
      }
    }).afterClosed().subscribe(result => {
      if (result && workout.id != null) {
        this.workoutService.deleteWorkout(workout.id).subscribe({
          next: () => {
            this.workouts = this.workouts.filter(w => w.id !== workout.id);
            this.snackBar.open('Trening został usunięty.', 'Zamknij', {
              duration: 3000,
              horizontalPosition: 'center',
              verticalPosition: 'bottom'
            });
          },
          error: (err) => {
            this.error = 'Nie udało się usunąć treningu.';
            console.error('Błąd usuwania treningu:', err);
            this.snackBar.open('Wystąpił błąd przy usuwaniu treningu.', 'Zamknij', {
              duration: 3000,
              horizontalPosition: 'center',
              verticalPosition: 'bottom'
            });
          }
        });
      }
    });
  }


}
