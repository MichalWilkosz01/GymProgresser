export interface WorkoutExercise {
  exerciseId: number;
  sets: number;
  reps: number;
  weightKg: number;
}

export interface WorkoutDetails {
  date: string;
  durationMin: number;
  note: string;
  exercises: WorkoutExercise[];
}
