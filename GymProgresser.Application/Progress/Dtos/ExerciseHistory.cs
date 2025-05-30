using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Progress.Dtos
{
    public class ExerciseHistory
    {
        public string ExerciseName { get; set; } = default!;
        public List<DataPoint> History { get; set; } = new List<DataPoint>(); // np. ciężar w czasie
    }
}
