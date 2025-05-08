using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.ExercisesWorkouts.Dtos
{
    public class GetExerciseWorkoutResponseDto
    {
        public string ExerciseName { get; set; } = null!;
        public int Sets { get; set; }
        public int Reps { get; set; }
        public double WeightKg { get; set; }
    }
}
