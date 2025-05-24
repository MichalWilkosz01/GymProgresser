import { Component, OnInit, Input } from '@angular/core';
import { ExerciseDetails } from '../exercise.model';
import { ExerciseService } from '../exercise.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-exercise-details',
  templateUrl: './exercise-details.component.html',
  styleUrl: './exercise-details.component.css'
})
export class ExerciseDetailsComponent implements OnInit {
  @Input() exerciseId!: number;
  exercise: ExerciseDetails | null = null;
  loading = true;
  error: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private exerciseService: ExerciseService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id) {
      this.exerciseService.getExerciseById(id).subscribe({
        next: (data) => { this.exercise = data, this.loading = false; },
        error: () => { this.loading = false, this.error = 'Nie udało się pobrać szczegółów.' }
      });
    }
  }
  goBack(): void {
    this.router.navigate(['/exercises']);
  }
}
