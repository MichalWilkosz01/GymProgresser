import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../core/api.service';
import { Exercise, ExerciseDetails } from './exercise.model';


@Injectable({ providedIn: 'root' })
export class ExerciseService {
    constructor(private api: ApiService) { }

    getExercises(): Observable<Exercise[]> {
        return this.api.get<Exercise[]>('exercises');
    }

    getExerciseById(id: number): Observable<ExerciseDetails> {
        return this.api.get<ExerciseDetails>(`exercises/${id}`);
    }
}
