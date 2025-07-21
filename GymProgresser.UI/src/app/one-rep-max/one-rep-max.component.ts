import { Component } from '@angular/core';
import { HttpParams } from '@angular/common/http';
import { ApiService } from '../core/api.service';

@Component({
  selector: 'app-one-rep-max',
  templateUrl: './one-rep-max.component.html',
  styleUrls: ['./one-rep-max.component.css']  // <- poprawka: "styleUrls", nie "styleUrl"
})
export class OneRepMaxComponent {
  constructor(private api: ApiService) {}

  weight: number = 1;
  reps: number = 1;
  result: number | null = null;
  errorMessage: string = '';
  isLoading: boolean = false;

  calculateOneRepMax(): void {
    this.errorMessage = '';
    this.result = null;
    console.log(this.weight);
    if (this.weight <= 0 || this.reps <= 0) {
      this.errorMessage = 'Wprowadź poprawne wartości większe niż 0.';
      return;
    }

    this.isLoading = true;

    const params = new HttpParams()
      .set('weight', this.weight.toString())
      .set('reps', this.reps.toString());

    this.api.post<number>('Progress/1-rep-max', null, { params }).subscribe({
      next: (res) => {
        this.result = res;
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'Wystąpił błąd podczas obliczania.';
        this.isLoading = false;
      }
    });
  }
}
