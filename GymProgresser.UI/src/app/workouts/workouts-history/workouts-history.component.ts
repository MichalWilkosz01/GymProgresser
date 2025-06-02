import { Component, OnInit, ViewChild } from '@angular/core';
import { ApiService } from '../../core/api.service';
import { ExercisePerformed } from './exercise-performed.model';
import { DataPoint } from './data-point.model';
import { ChartConfiguration, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-workouts-history',
  templateUrl: './workouts-history.component.html',
  styleUrls: ['./workouts-history.component.css']
})
export class WorkoutsHistoryComponent implements OnInit {
  exercises: ExercisePerformed[] = [];
  selectedExerciseId: number | null = null;
  forecastLength: number = 3;
  @ViewChild(BaseChartDirective) chart?: BaseChartDirective;

  readonly performedExercisesEndpoint = 'Exercises/performed';
  readonly exerciseHistoryEndpoint = 'Progress/history';
  getExerciseHistoryEndpoint = (exerciseId: number) => `Progress/history/${exerciseId}`;
  exerciseName: string = '';
  history: DataPoint[] = [];


  chartData: ChartConfiguration<'line'>['data'] = {
    labels: [],
    datasets: []
  };

  chartOptions: ChartConfiguration['options'] = {
    responsive: true,
    devicePixelRatio: window.devicePixelRatio || 1,
    plugins: {
      legend: { display: false },
      tooltip: {
        enabled: true,
        callbacks: {
          label: (tooltipItem) => {
            const point = tooltipItem.raw as DataPoint;
            return `${point.reps} x ${point.weightKg} kg (${point.sets} serie)`;
          }
        }
      },
      title: {
        display: true,
        text: `Historia `,
        font: {
          size: 18
        }
      }
    },

    scales: {
      x: {
        type: 'linear',
        title: {
          display: true,
          text: 'Ilość treningów'
        }
      },
      y: {
        beginAtZero: true,
        title: {
          display: true,
          text: 'Objętość treningowa (kg)'
        }
      }
    }
  };

  chartType: ChartType = 'line';


  constructor(private api: ApiService) { }

  ngOnInit(): void {
    this.api.get<ExercisePerformed[]>(this.performedExercisesEndpoint).subscribe(result => {
      this.exercises = result;

      // Domyślny wybór pierwszego ćwiczenia (jeśli istnieje)
      if (this.exercises.length > 0) {
        this.selectedExerciseId = this.exercises[0].id;
      }
    });
  }

  showHistory(): void {
    if (!this.selectedExerciseId) return;

    const url = `${this.exerciseHistoryEndpoint}/${this.selectedExerciseId}`;
    this.api
      .get<{ exerciseName: string; history: DataPoint[] }>(url)
      .subscribe(response => {
        this.exerciseName = response.exerciseName;
        this.history = response.history;

        // aktualizacja wykresu
        this.chartData = {
          labels: this.history.map(p => `${p.x}`),
          datasets: [
            {
              data: this.history,
              label: `${this.exerciseName}`,
              fill: false,
              tension: 0.3
            }
          ]
        };
        if (this.chartOptions) {
          this.chartOptions.plugins = {
            ...this.chartOptions.plugins,
            title: {
              display: true,
              text: `Historia: ${this.exerciseName}`,
              font: { size: 18 }
            }
          };
        }
      });
  }

  showForecast(): void {
  if (!this.selectedExerciseId) return;

  const endpoint = `Progress/${this.selectedExerciseId}/predict`;
  const params = new HttpParams().set('predictionPoints', this.forecastLength);

  this.api.get<any[]>(endpoint, params).subscribe({
    next: (data) => {
      console.log('Dane prognozy:', data);

      if (!Array.isArray(data)) {
        console.error('Nieprawidłowa odpowiedź z serwera – oczekiwano tablicy punktów.');
        return;
      }

      const regressionDataset = {
        label: 'Regresja liniowa',
        data: data.map(point => ({ x: point.x, y: point.y })),
        borderColor: '#888',
        borderDash: [5, 5],
        fill: false,
        pointRadius: 0,
        tension: 0.3
      };

      if (this.chart?.chart) {
        this.chart.chart.data.labels = []; // Niepotrzebne przy danych x/y, ale dla porządku
        this.chart.chart.data.datasets = [regressionDataset];
        this.chart.update();
      }
    },
    error: (err) => {
      console.error('Błąd podczas pobierania danych prognozy:', err);
    }
  });
}




}
