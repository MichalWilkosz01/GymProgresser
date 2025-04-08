using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Domain.Entities
{
    public class BodyMeasurement
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public DateTime Date { get; set; }
        public double WeightKg { get; set; }
        public double BodyFatPrecent { get; set; }
        public int HeightCm { get; set; }
        public int Age { get; set; }
    }
}
