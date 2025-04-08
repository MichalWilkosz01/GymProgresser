using GymProgresser.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Domain.Entities
{
    public class Exercise
    {
        public int Id { get; set; }
        public bool IsVerified { get; set; }
        public string Name { get; set; } = default!;
        public ExerciseCategory Category { get; set; }
    }
}
