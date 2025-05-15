using GymProgresser.Application.Exercises.Interfaces;
using GymProgresser.Domain.Entities;
using GymProgresser.Domain.Enums;
using GymProgresser.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Infrastructure.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly GymProgressDbContext _dbContext;
        public ExerciseRepository(GymProgressDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Exercise>?> GetAllExercisesAsync()
        {
            var res = await _dbContext.Exercises.ToListAsync();
            return res;
        }

        public async Task<List<Exercise>?> GetAllExercisesByCategoryAsync(ExerciseCategory category)
        {
            var res = await _dbContext.Exercises.Where(e => e.Category == category).ToListAsync();
            return res;
        }

        public async Task<List<Exercise>?> GetAllExercisesInWorkoutAsync(int workoutId)
        {
            var res = await _dbContext.Exercises
                                .Where(e => e.ExercisesWorkout != null && e.ExercisesWorkout.Any(ew => ew.WorkoutId == workoutId))
                                .ToListAsync();
            return res;
        }

        public async Task<List<Exercise>?> GetAllVerifiedExercisesAsync()
        {
            var res = await _dbContext.Exercises.Where(e => e.IsVerified).ToListAsync();
            return res;
        }

        public async Task<List<Exercise>?> GetAllVerifiedExercisesByCategoryAsync(ExerciseCategory category)
        {
            var res = await _dbContext.Exercises.Where(e => e.IsVerified && e.Category == category).ToListAsync();
            return res;
        }

        public async Task<Exercise?> GetExerciseAsync(int exerciseId)
        {
            var res = await _dbContext.Exercises.FindAsync(exerciseId);
            return res;
        }

        public async Task PostExerciseAsync(Exercise exercise, int userId)
        {
            await _dbContext.AddAsync(exercise);
            await _dbContext.SaveChangesAsync();
        }
    }
}
