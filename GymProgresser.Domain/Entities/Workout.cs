using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Domain.Entities
{
    public class Workout
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public double DurationMin { get; set; }
        public string? Note { get; set; }
        public List<ExerciseWorkout>? ExerciseWorkouts { get; set; }
    }
}
