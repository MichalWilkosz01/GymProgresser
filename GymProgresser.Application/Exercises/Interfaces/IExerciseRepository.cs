using GymProgresser.Domain.Entities;
using GymProgresser.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Application.Exercises.Interfaces
{
    public interface IExerciseRepository
    {
        public Task PostExerciseAsync(Exercise exercise, int userId);
        public Task<Exercise?> GetExerciseAsync(int exerciseId);
        public Task<List<Exercise>?> GetAllExercisesByCategoryAsync(ExerciseCategory category);
        public Task<List<Exercise>?> GetAllExercisesAsync();
        public Task<List<Exercise>?> GetAllVerifiedExercisesAsync();
        public Task<List<Exercise>?> GetAllVerifiedExercisesByCategoryAsync(ExerciseCategory category);
        public Task<List<Exercise>?> GetAllExercisesInWorkoutAsync(int workoutId);
    }
}
