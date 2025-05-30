using GymProgresser.Application.ExercisesWorkouts.Dtos;
using GymProgresser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Workouts.Dtos
{
    public class GetWorkoutResponseDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double DurationMin { get; set; }
        public string? Note { get; set; }
        public List<GetExerciseWorkoutResponseDto> Exercises { get; set; } = [];
    }
}
