import { Component, OnInit, ViewChild } from '@angular/core';
import { ApiService } from '../../core/api.service';
import { ExercisePerformed } from './exercise-performed.model';
import { DataPoint } from './data-point.model';
import { ChartConfiguration, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { HttpParams } from '@angular/common/http';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import autoTable from 'jspdf-autotable';
import {formatDate} from '@angular/common';

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
            const datasetLabel = tooltipItem.dataset.label;

            // Dla regresji liniowej – pokazujemy wartości (X, Y)
            if (datasetLabel === 'Przewidywany progres') {
              const x = tooltipItem.parsed.x;
              const y = tooltipItem.parsed.y;
              return `Przewidywana objętość treningowa: ${y.toFixed(1)}kg`;
            }

            // Dla danych historycznych – klasyczny format reps x weight
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

    this.api.get<{ slope: number, intercept: number }>(endpoint, params).subscribe({
      next: (data) => {
        console.log('Współczynniki regresji:', data);

        if (!this.chart?.chart?.data?.datasets?.length) {
          console.error('Brak danych wejściowych na wykresie.');
          return;
        }

        // Zakładamy, że dane użytkownika są w pierwszym dataset
        const userData = this.chart.chart.data.datasets[0]?.data ?? [];
        const historyLength = userData.length;

        const totalLength = historyLength + this.forecastLength;
        const labels = Array.from({ length: totalLength }, (_, i) => i + 1);
        const regression = labels.map(x => data.slope * x + data.intercept);

        const regressionDataset = {
          label: 'Przewidywany progres',
          data: regression,
          borderColor: '#888',
          pointBackgroundColor: '#888',
          pointBorderColor: '#888',
          borderDash: [5, 5],
          fill: false,
          pointRadius: 3, // zamiast 0
          pointHoverRadius: 5,
          pointHitRadius: 10,
          tension: 0.3
        };


        // Usuń poprzednią regresję, jeśli istnieje
        const filteredDatasets = this.chart.chart.data.datasets.filter(ds => ds.label !== 'Przewidywany progres');

        // Nadpisz labels i datasets
        this.chart.chart.data.labels = labels;
        this.chart.chart.data.datasets = [
          ...filteredDatasets,
          regressionDataset
        ];

        this.chart.update();
      },
      error: (err) => {
        console.error('Błąd podczas pobierania współczynników regresji:', err);
      }
    });
  }



  removeForecast(): void {
    if (!this.chart?.chart?.data?.datasets?.length) return;

    const regressionLabel = 'Przewidywany progres';

    // Usuń dataset regresji
    this.chart.chart.data.datasets = this.chart.chart.data.datasets.filter(
      ds => ds.label !== regressionLabel
    );

    // Przywróć oryginalne długości labels, jeśli zostały rozszerzone
    const userData = this.chart.chart.data.datasets[0]?.data ?? [];
    this.chart.chart.data.labels = userData.map((_, i) => i + 1);

    this.chart.update();
  }

  saveChartAsPDF(): void {
    const chartElement = document.querySelector('.chart-container') as HTMLElement;

    if (!chartElement) {
      console.error('Nie znaleziono wykresu do zapisania.');
      return;
    }

    html2canvas(chartElement).then(canvas => {
      const imgData = canvas.toDataURL('image/png');
      const pdf = new jsPDF('p', 'mm', 'a4');

      const pageWidth = pdf.internal.pageSize.getWidth();
      const imgProps = pdf.getImageProperties(imgData);
      const imgWidth = pageWidth - 20;
      const imgHeight = (imgProps.height * imgWidth) / imgProps.width;

      pdf.setFontSize(14);
      const now = formatDate(new Date(), 'dd.MM.yyyy', 'pl');
      pdf.text(`Wygenerowano: ${now}`, 10, 10);
      pdf.addImage(imgData, 'PNG', 10, 15, imgWidth, imgHeight);

      const startY = 20 + imgHeight + 10;

      // 🔢 Dane do tabeli z numeracją
      const tableBody = this.history.map((point, index) => [
        index + 1,
        //point.date,
        `${point.reps * point.weightKg * point.sets} kg`,
        `${point.sets}`,
        `${point.reps}`
      ]);

      autoTable(pdf, {
        head: [['Lp.', 'Objetosc treningowa (kg)', 'Serie', 'Powtorzenia']],
        body: tableBody,
        startY: startY,
        theme: 'grid', // ✅ automatyczne linie siatki poziome + pionowe
        styles: {
          fontSize: 10,
          font: 'Inter'
        },
        headStyles: {
          font: 'Inter',
          fontStyle: 'bold',
          fillColor: [230, 230, 230],
          textColor: [0, 0, 0] // ✅ ciemna czcionka w nagłówku
        }
      });


      pdf.save(`Historia ${this.exerciseName}.pdf`);
    });
  }

}
