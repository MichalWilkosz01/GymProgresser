using GymProgresser.Application.ExercisesWorkouts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Workouts.Dtos
{
    public class PostWorkoutRequestDto
    {
        public DateTime Date { get; set; }
        public double DurationMin { get; set; }
        public string? Note { get; set; }
        public List<ExerciseWorkoutDto> Exercises { get; set; } = [];
    }
}
