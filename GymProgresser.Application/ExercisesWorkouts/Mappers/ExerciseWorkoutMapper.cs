using GymProgresser.Application.ExercisesWorkouts.Dtos;
using GymProgresser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.ExercisesWorkouts.Mappers
{
    public static class ExerciseWorkoutMapper
    {
        public static GetExerciseWorkoutResponseDto GetExerciseWorkoutResponseDto(ExerciseWorkout exercisesWorkout)
        {
            return new GetExerciseWorkoutResponseDto()
            {
                ExerciseName = exercisesWorkout.Exercise.Name,
                Sets = exercisesWorkout.Sets,
                Reps = exercisesWorkout.Reps,
                WeightKg = exercisesWorkout.WeightKg
            };
        }

        public static ExerciseWorkout ExerciseWorkoutFromDto(ExerciseWorkoutDto dto)
        {
            return new ExerciseWorkout()
            {
                ExerciseId = dto.ExerciseId,
                Sets = dto.Sets,
                Reps = dto.Reps,
                WeightKg = dto.WeightKg
            };
        }
    }
}
