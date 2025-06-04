import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Exercise } from '../../../exercises/exercise.model';
import { ApiService } from '../../../core/api.service';
import { ActivatedRoute, Router } from '@angular/router';
import { WorkoutService } from '../../workout.service';
import { WorkoutDetails } from '../../workout-exercises.model';
import { MaterialModule } from '../../../shared/material.module';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-workout-form',
  templateUrl: './workout-form.component.html',
  styleUrl: './workout-form.component.css'
})
export class WorkoutFormComponent implements OnInit {
  workoutForm: FormGroup;
  exercises: Exercise[] = [];
  isEditMode = false;


  constructor(private fb: FormBuilder, private api: ApiService, private route: ActivatedRoute, private workoutService: WorkoutService, private router: Router, private snackBar: MatSnackBar) {
    this.workoutForm = this.fb.group({
      note: [''],
      date: [null, Validators.required],
      //time: ['', Validators.required],
      durationMin: [null, [Validators.min(1)]],
      exercises: this.fb.array([])
    });
  }

  ngOnInit(): void {
    this.loadExercises();

    const workoutId = this.route.snapshot.paramMap.get('id');
    if (workoutId) {
      this.isEditMode = true;
      this.loadWorkout(+workoutId);
    }

  }

  loadExercises(): void {
    this.api.get<Exercise[]>('exercises').subscribe({
      next: (data) => (this.exercises = data),
      error: (err) => console.error('Błąd podczas pobierania ćwiczeń:', err)
    });
  }

  get exercisesFormArray(): FormArray {
    return this.workoutForm.get('exercises') as FormArray;
  }

  addExercise(): void {
    const exerciseGroup = this.fb.group({
      exerciseId: ['', Validators.required],
      sets: [1, [Validators.required, Validators.min(1)]],
      reps: [10, [Validators.required, Validators.min(1)]],
      weightKg: [0, [Validators.required, Validators.min(0.1)]]
    });

    this.exercisesFormArray.push(exerciseGroup);
  }

  removeExercise(index: number): void {
    this.exercisesFormArray.removeAt(index);
  }

  onSubmit(): void {
    if (this.workoutForm.invalid) return;

    const workoutData = this.workoutForm.value;
    const date: Date = this.workoutForm.value.date;
    const formattedDate = date.toLocaleDateString('en-CA'); // np. "2025-06-04"
    workoutData.date = formattedDate;

    if (this.isEditMode) {
      const workoutId = this.route.snapshot.paramMap.get('id');
      if (workoutId) {
        const id = +workoutId;
        const workoutToSend = { id, ...workoutData };

        this.workoutService.updateWorkout(id, workoutToSend).subscribe({
          next: () => {
            this.snackBar.open('Trening zaktualizowany pomyślnie!', 'Zamknij', {
              duration: 3000
            });
            this.router.navigate(['/workouts']);
          },
          error: err => {
            console.error('Błąd aktualizacji:', err);
            this.snackBar.open('Błąd podczas aktualizacji treningu.', 'Zamknij', {
              duration: 3000
            });
          }
        });
      }
    } else {
      const workoutToSend = { ...workoutData };
      console.log(workoutToSend);

      this.api.post('workouts', workoutToSend).subscribe({

        next: () => {
          this.snackBar.open('Trening dodany pomyślnie!', 'Zamknij', {
            duration: 3000
          });
          this.router.navigate(['/workouts']);
        },
        error: err => {
          console.error('Błąd dodawania:', err);
          this.snackBar.open('Błąd podczas dodawania treningu.', 'Zamknij', {
            duration: 3000
          });
        }
      });
    }
  }


  loadWorkout(id: number): void {
    this.workoutService.getWorkoutById(id).subscribe({
      next: (workout: WorkoutDetails) => {
        this.workoutForm.patchValue({
          note: workout.note,
          date: new Date(workout.date),
          durationMin: workout.durationMin
        });

        const exercisesFG = workout.exercises.map(ex =>
          this.fb.group({
            exerciseId: [ex.exerciseId, Validators.required],
            sets: [ex.sets, [Validators.required, Validators.min(1)]],
            reps: [ex.reps, [Validators.required, Validators.min(1)]],
            weightKg: [ex.weightKg, [Validators.required, Validators.min(0.1)]]
          })
        );

        const fa = this.fb.array(exercisesFG);
        this.workoutForm.setControl('exercises', fa);
      },
      error: (err) => console.error('Błąd ładowania treningu:', err)
    });
  }
}
