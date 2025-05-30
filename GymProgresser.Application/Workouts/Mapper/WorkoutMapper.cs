using GymProgresser.Application.ExercisesWorkouts.Mappers;
using GymProgresser.Application.Workouts.Dtos;
using GymProgresser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Workouts.Mapper
{
    public static class WorkoutMapper
    {
        public static Workout WorkoutRequestDtoToWorkout(PostWorkoutRequestDto requestDto)
        {
            return new Workout
            {
                Date = requestDto.Date,
                DurationMin = requestDto.DurationMin,
                Note = requestDto.Note,
                ExercisesWorkout = requestDto.Exercises.Select(e => new ExerciseWorkout
                {
                    ExerciseId = e.ExerciseId,
                    Sets = e.Sets,
                    Reps = e.Reps,
                    WeightKg = e.WeightKg
                }).ToList()
            };
        }

        public static void UpdateWorkoutFromDto(Workout workout, UpdateWorkoutRequestDto requestDto)
        {
            workout.Date = requestDto.Date;
            workout.DurationMin = requestDto.DurationMin;
            workout.Note = requestDto.Note;
            workout.ExercisesWorkout = requestDto.Exercises
                                    .Select(e => ExerciseWorkoutMapper.ExerciseWorkoutFromDto(e))
                                    .ToList();
        }

        public static GetWorkoutResponseDto GetWorkoutDtoFromWorkout(Workout workout)
        {
            return new GetWorkoutResponseDto
            {
                Id = workout.Id,
                Date = workout.Date,
                DurationMin = workout.DurationMin,
                Note = workout.Note,
                Exercises = workout.ExercisesWorkout
                                .Select(ew => ExerciseWorkoutMapper.GetExerciseWorkoutResponseDto(ew))
                                .ToList()
            };
        }
    }
}
