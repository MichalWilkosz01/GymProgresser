using GymProgresser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.ExercisesWorkouts.Interfaces
{
    public interface IExerciseWorkoutRepository
    {
        public Task<List<ExerciseWorkout>> GetUserExerciseWorkoutByExerciseAsync(int userId, int exerciseId);
    }
}
