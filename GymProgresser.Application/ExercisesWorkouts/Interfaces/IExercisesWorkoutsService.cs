using GymProgresser.Application.ExercisesWorkouts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.ExercisesWorkouts.Interfaces
{
    public interface IExercisesWorkoutsService
    {
        public Task<List<GetExerciseWorkoutResponseDto>?> GetUserExercisesWorkoutsAsync(int userId);
    }
}
