import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';

@Component({
    selector: 'app-confirm-dialog',
    standalone: true,
    template: `
    <h2 mat-dialog-title>Potwierdź usunięcie</h2>
    <mat-dialog-content>{{ data.message }}</mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button (click)="dialogRef.close(false)">Anuluj</button>
      <button mat-button color="warn" (click)="dialogRef.close(true)">Usuń</button>
    </mat-dialog-actions>
  `,
    imports: [MatDialogModule,
        MatButtonModule]
})
export class ConfirmDialogComponent {
    dialogRef = inject(MatDialogRef<ConfirmDialogComponent>);
    data = inject(MAT_DIALOG_DATA);
}
