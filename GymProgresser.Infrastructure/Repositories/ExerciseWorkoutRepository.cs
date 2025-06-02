using GymProgresser.Application.ExercisesWorkouts.Interfaces;
using GymProgresser.Domain.Entities;
using GymProgresser.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Infrastructure.Repositories
{
    public class ExerciseWorkoutRepository : IExerciseWorkoutRepository
    {
        private readonly GymProgressDbContext _dbContext;
        public ExerciseWorkoutRepository(GymProgressDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<ExerciseWorkout>> GetUserExerciseWorkoutByExerciseAsync(int userId, int exerciseId)
        {
            var res = await _dbContext.ExercisesWorkouts
                        .Where(ew => ew.ExerciseId == exerciseId && ew.Workout.UserId == userId)
                        .Include(ew => ew.Workout)
                        .Include(ew => ew.Exercise)
                        .ToListAsync();

            return res;
        }
    }
}
