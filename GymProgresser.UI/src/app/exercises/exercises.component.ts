import { Component, OnInit } from '@angular/core';
import { Exercise, ExerciseDetails } from './exercise.model';
import { ExerciseService } from './exercise.service';
import { Observable, catchError, of } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-exercises',
  templateUrl: './exercises.component.html',
  styleUrl: './exercises.component.css'
})
export class ExercisesComponent implements OnInit {
  exercises$!: Observable<Exercise[]>;
  error: string | null = null;
  selectedExercise: ExerciseDetails | null = null;

  constructor(private exerciseService: ExerciseService, private router: Router) { }

  ngOnInit(): void {
    this.exercises$ = this.exerciseService.getExercises().pipe(
      catchError(err => {
        this.error = 'Nie udało się pobrać listy treningów.';
        return of([]);
      })
    );
  }



  onExerciseClick(id: number): void {
    this.router.navigate(['/exercises', id]);
  }

}