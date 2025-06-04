export interface WorkoutExercise {
  exerciseId: number;
  exerciseName: string;
  sets: number;
  reps: number;
  weightKg: number;
}

export interface WorkoutDetails {
  id?: number;
  date: string;
  durationMin: number;
  note: string;
  exercises: WorkoutExercise[];
}
