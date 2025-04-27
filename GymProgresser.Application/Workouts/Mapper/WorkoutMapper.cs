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
        public static Workout WorkoutRequestDtoToWorkout(WorkoutRequestDto requestDto)
        {
            return new Workout
            {
                Date = requestDto.Date,
                DurationMin = requestDto.DurationMin,
                Note = requestDto.Note,
            };
        }

        public static void UpdateWorkoutFromDto(Workout workout, WorkoutRequestDto requestDto)
        {
            workout.Date = requestDto.Date;
            workout.DurationMin = requestDto.DurationMin;
            workout.Note = requestDto.Note;
        }
    }
}
