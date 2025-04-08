using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Domain.Entities
{
    public class ExerciseWorkout
    {
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public int WorkoutId { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public double WeightKg { get; set; }
    }
}
