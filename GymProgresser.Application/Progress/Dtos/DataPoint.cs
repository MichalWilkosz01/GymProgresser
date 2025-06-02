using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Progress.Dtos
{
    public class DataPoint
    {
        public double X { get; set; } // number exercise
        public double Y { get; set; } // training volume
        public int Reps { get; set; }
        public int Sets { get; set; }
        public double WeightKg { get; set; } 
    }
}
