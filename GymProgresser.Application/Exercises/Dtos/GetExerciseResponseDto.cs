using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Exercises.Dtos
{
    public class GetExerciseResponseDto
    {
        public string Name { get; set; } = default!;
        public string Category { get; set; } = default!;
        public string Label { get; set; } = default!;
        public bool IsVerified { get; set; }
    }
}
