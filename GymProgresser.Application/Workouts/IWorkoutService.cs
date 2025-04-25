using GymProgresser.Application.Workouts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Workouts
{
    public interface IWorkoutService
    {
        public Task PostWorkoutAsync(WorkoutRequestDto registerRequestDto);
    }
}
