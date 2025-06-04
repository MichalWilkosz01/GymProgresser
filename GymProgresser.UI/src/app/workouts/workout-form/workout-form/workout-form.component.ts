import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Exercise } from '../../../exercises/exercise.model';
import { ApiService } from '../../../core/api.service';

@Component({
  selector: 'app-workout-form',
  templateUrl: './workout-form.component.html',
  styleUrl: './workout-form.component.css'
})
export class WorkoutFormComponent implements OnInit {
  workoutForm: FormGroup;
  exercises: Exercise[] = [];

  constructor(private fb: FormBuilder, private api: ApiService) {
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
    if (this.workoutForm.valid) {
      const workoutData = this.workoutForm.value;
      console.log('Nowy trening:', workoutData);
      this.api.post('workouts', workoutData).subscribe({
        next: (data) => (console.log('ok')),
        error: (err) => console.error('Błąd podczas pobierania ćwiczeń:', err)
      });
      // Tu można dodać wysyłkę do backendu:
      // this.http.post('/api/workouts', workoutData).subscribe(...)
    }
  }
}
