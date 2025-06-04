import { WorkoutDetails } from "./workout-exercises.model";

export interface WorkoutWithState extends WorkoutDetails {
  showExercises?: boolean;
  menuOpen?: boolean;
}