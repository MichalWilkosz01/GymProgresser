using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Workouts.Dtos
{
    public class WorkoutRequestDto
    {
        public int? Id { get; set; }
        public DateTime Date { get; set; }
        public double DurationMin { get; set; }
        public string? Note { get; set; }
    }
}
