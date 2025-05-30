export interface WorkoutExercise {
  exerciseName: string;
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
